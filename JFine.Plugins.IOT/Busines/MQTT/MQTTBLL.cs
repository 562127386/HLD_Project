﻿
/********************************************************************************
**文 件 名:ModbusGatewayBLL
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:55:26
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JFine.Cache;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using JFine.Busines.SystemManage;
using JFine.Data.Common;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using JFine.Plugins.IOT.IOTHubs;
using JFine.Domain.Models.SystemManage;
using JFine.Busines.SystemManage;
using MQTTnet;
using MQTTnet.Diagnostics;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System.Threading.Tasks;
using System.Diagnostics;
using JFine.Plugins.IOT.Busines.IOT;
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Plugins.IOT.Domain.Models.MQTT;
using Newtonsoft.Json.Linq;

namespace JFine.Plugins.IOT.Busines.MQTT
{	
	/// <summary>
    /// ModbusBLL
	/// </summary>	
    public class MQTTBLL
	{
        private GatewayValBLL gatewayValBLL = new GatewayValBLL();
        //采集数值队列
        private static Queue<GatewayValEntity> gateWayValQueue = new Queue<GatewayValEntity>(); 
        //网关列表
        public static List<MQTTGatewayEntity> gatewayList = new List<MQTTGatewayEntity>();
        //网关Tag列表
        public static List<MQTTGatewayTagEntity> gatewayTagList = new List<MQTTGatewayTagEntity>();
        //MQTT服务是否被启动过
        private static bool flag = false;
        //MqttServer
        private static MqttServer mqttServer = null;
        public static int ServerState = 0;//-1:启动异常；0:未启动；1：已启动；2：已停止

        private static ModbusHub modbusHub = new ModbusHub();

        private static IOTDeviceBeatBLL iOTDeviceBeatBLL = new IOTDeviceBeatBLL();
    

        /// <summary>
        /// 启动MQTT服务
        /// </summary>
        public void MQTTStart()
        {

            if (ServerState == -1 || ServerState == 0) 
            {
                MQTTGatewayBLL mQTTGatewayBll = new MQTTGatewayBLL();
                MQTTGatewayTagBLL mQTTGatewayTagBll = new MQTTGatewayTagBLL();
                gatewayList = mQTTGatewayBll.GetList().ToList();
                gatewayTagList = mQTTGatewayTagBll.GetList().ToList();
                Task.Run(async () => { await StartMqttServer(); });
                //flag = true;
                ServerState = 1;
                StartValQueue();
                iOTDeviceBeatBLL.StartValQueue();
            }
            else if (ServerState == 2) 
            {
                Task.Run(async () => { await StartMqttServer(); });
            }
            
        }

        /// <summary>
        /// 停止MQTT服务
        /// </summary>
        public void MQTTStop()
        {

            if (ServerState == 1)
            {
                //Task.Run(async () => { await mqttServer.StopAsync(); });
                mqttServer.StopAsync();
                ServerState = 2;
            }

        }

        private async Task StartMqttServer()
        {
             try
                {
                    var optionsBuilder = new MqttServerOptionsBuilder()
                                .WithConnectionBacklog(100)
                                .WithDefaultEndpointPort(1883)
                                .WithConnectionValidator(ValidatingMqttClients())
                                ;
                    if (mqttServer == null)
                    {
                            // Start a MQTT server.
                            mqttServer = new MqttFactory().CreateMqttServer() as MqttServer;
                            mqttServer.ApplicationMessageReceived += MqttServer_ApplicationMessageReceived;
                            mqttServer.ClientConnected += MqttServer_ClientConnected;
                            mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;

                            mqttServer.Started += MqttServer_Started;
                            mqttServer.Stopped += MqttServer_Stopped;

                            Task.Run(async () => { await mqttServer.StartAsync(optionsBuilder.Build()); });
                            //mqttServer.StartAsync(optionsBuilder.Build());
                            Trace.TraceInformation("MQTT服务启动成功！");
                            ServerState = 1;
                
                    }
                    else if (ServerState == 2)
                    {
                        Task.Run(async () => { await mqttServer.StartAsync(optionsBuilder.Build()); });
                        Trace.TraceInformation("MQTT服务启动成功！");
                        ServerState = 1;
                    }
                }
             catch (Exception ex)
             {
                 ServerState = -1;
                 //记录日志
                 LogEntity logEntity = new LogEntity();
                 logEntity.Category = "服务";
                 logEntity.OperateType = "MQTT启动异常";
                 logEntity.CreateUserId = "MQTT";
                 logEntity.CreateUserName = "MQTT";
                 logEntity.Module = "MQTT";
                 logEntity.ModuleId = "";
                 logEntity.ExecuteResult = -1;
                 logEntity.Description = ex.Message; 
                 logEntity.CreateDate = DateTime.Now;
                 logEntity.SourceContentJson = ex.StackTrace;
                 logEntity.Mark = "";
                 logEntity.AddLog();
                 return;
             }
            
             Trace.TraceInformation("MQTT服务启动成功！");
        }
        private static void MqttServer_Started(object sender, EventArgs e)
        {
            ServerState = 1;
        }

        private static void MqttServer_Stopped(object sender, EventArgs e)
        {
            ServerState = 2;
        }

        private static void MqttServer_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
             var gateway = gatewayList.FirstOrDefault(t => t.ClientId.Equals(e.ClientId));
             gateway.Status = 1;
        }

        private static void MqttServer_ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
             var gateway = gatewayList.FirstOrDefault(t => t.ClientId.Equals(e.ClientId));
             gateway.Status = 0;
        }

        private static void MqttServer_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                var gateway = gatewayList.FirstOrDefault(t => t.ClientId.Equals(e.ClientId));
                List<MQTTGatewayTagEntity> tagList_temp = gatewayTagList.Where(t => t.BindId.Equals(gateway.Id)).ToList();
                var payloadString = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                var payload = payloadString.ToJObject();
                //查询条件
                if (!payload["tags"].IsEmpty())
                {
                    var tags = payload["tags"];
                    DateTime dt = DateTime.Now;
                    if (!payload["timestamp"].IsEmpty())
                    {
                        DateTime timestamp = DateTime.Parse(payload["timestamp"].ToString().Replace("T", " ").Replace("Z", ""));
                        dt = timestamp;
                    }
                    foreach (var tagEntity in tagList_temp)
                    {
                        if (!tags[tagEntity.ParameterCode].IsEmpty())
                        {
                            if (tagEntity.ParameterType != null && IOTConstant.SWITCH_VALUE == tagEntity.ParameterType)
                            {
                                #region 开关量处理
                                //ModbusHub modbusHub = new ModbusHub();
                                if (ModbusHub.modbusData.ContainsKey(tagEntity.DeviceCode + "@_@" + tagEntity.ParameterCode))
                                {
                                    string preValue = ModbusHub.modbusData[tagEntity.DeviceCode + "@_@" + tagEntity.ParameterCode].ToString();
                                    if (!(preValue.Equals(tags[tagEntity.ParameterCode].ToString())))
                                    {
                                        SaveDataAndNotice(tagEntity, gateway, tags, dt);
                                        SignalDeal(tagEntity, gateway, tags, dt);
                                    }
                                }
                                else
                                {
                                    SaveDataAndNotice(tagEntity, gateway, tags, dt);
                                    SignalDeal(tagEntity, gateway, tags, dt);
                                }
                                #endregion

                                
                            }
                            else if (tagEntity.ParameterType != null && IOTConstant.ANALOG_VALUE == tagEntity.ParameterType)
                            {
                                #region 模拟量
                                SaveDataAndNotice(tagEntity, gateway, tags, dt);
                                #endregion
                            }
                            
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                //记录日志
                LogEntity logEntity = new LogEntity();
                logEntity.Category = "服务";
                logEntity.OperateType = "MQTT接收数据异常";
                logEntity.CreateUserId = "MQTT";
                logEntity.CreateUserName = "MQTT";
                logEntity.Module = "MQTT";
                logEntity.ModuleId = "";
                logEntity.ExecuteResult = -1;
                logEntity.Description = ex.Message;
                logEntity.CreateDate = DateTime.Now;
                logEntity.SourceContentJson = ex.StackTrace;
                logEntity.Mark = "";

                logEntity.AddLog();
            }
        }

        /// <summary>
        /// 保存数据并发送数据到前端
        /// </summary>
        /// <param name="tagEntity"></param>
        /// <param name="gateway"></param>
        /// <param name="tags"></param>
        /// <param name="dt"></param>
        private static void SaveDataAndNotice(MQTTGatewayTagEntity tagEntity,MQTTGatewayEntity gateway,JToken tags,DateTime dt)
        {
            GatewayValEntity gatewayVal = new GatewayValEntity();
            gatewayVal.Id = Sequence.Sequence.getSnowflakeID();
            gatewayVal.BindId = gateway.Id;
            gatewayVal.DeviceCode = tagEntity.DeviceCode;
            gatewayVal.ParameterCode = tagEntity.ParameterCode;
            gatewayVal.ParameterName = tagEntity.ParameterName;
            gatewayVal.ParameterValue = tags[tagEntity.ParameterCode].ToString();
            gatewayVal.ParameterType = tagEntity.ParameterType;
            gatewayVal.ParameterCategory = tagEntity.ParameterCategory;
            gatewayVal.ParameterDate = dt;
            
            gatewayVal.CreateDate = DateTime.Now;
            gateWayValQueue.Enqueue(gatewayVal);
            ModbusHub.modbusData[gatewayVal.DeviceCode + "@_@" + gatewayVal.ParameterCode] = gatewayVal.ParameterValue;
            modbusHub.SendDeviceData(gatewayVal.DeviceCode, gatewayVal.ParameterType.Value, gatewayVal.ParameterCategory.Value, gatewayVal.ParameterCode, gatewayVal.ParameterValue, dt);
        }

        /// <summary>
        /// 信号处理
        /// </summary>
        /// <param name="tagEntity">网关参数</param>
        /// <param name="gateway">网关数据</param>
        /// <param name="tags">信号数据</param>
        private static void SignalDeal(MQTTGatewayTagEntity tagEntity, MQTTGatewayEntity gateway, JToken tags, DateTime dt) 
        {
            if (tagEntity.ParameterCategory != null)
            {
                switch (tagEntity.ParameterCategory)
                {
                    // 合模
                    case (int)IOTConstant.ParameterCategory.MouldOff:
                        iOTDeviceBeatBLL.MouldOff(gateway.Id, tagEntity.DeviceCode, tags[tagEntity.ParameterCode].ToString(), dt);
                        break;
                    //射胶
                    case (int)IOTConstant.ParameterCategory.Shot:
                        iOTDeviceBeatBLL.Shot(gateway.Id, tagEntity.DeviceCode, tags[tagEntity.ParameterCode].ToString());
                        break;
                    //马达开
                    case (int)IOTConstant.ParameterCategory.MotorOn:
                        IOTDeviceBLL iOTDeviceBLL = new IOTDeviceBLL();
                        iOTDeviceBLL.MotorOn(gateway.Id, tagEntity.DeviceCode, tags[tagEntity.ParameterCode].ToString());
                        break;
                    //马达关
                    case (int)IOTConstant.ParameterCategory.MotorOff:
                        IOTDeviceBLL iOTDeviceBLL_OFF = new IOTDeviceBLL();
                        iOTDeviceBLL_OFF.MotorOff(gateway.Id, tagEntity.DeviceCode, tags[tagEntity.ParameterCode].ToString());
                        break;
                    default:
                     break;
                

                }
            }
        }

        /// <summary>
        /// mqtt客户端验证
        /// </summary>
        /// <returns></returns>
        private static Action<MqttConnectionValidatorContext> ValidatingMqttClients()
        {
            // Setup client validator.    
            var options =new MqttServerOptions();
            options.ConnectionValidator = c =>
            {
                var gateway = gatewayList.FirstOrDefault(t=>t.ClientId.Equals(c.ClientId) && t.UserName.Equals(c.Username) && t.PWD.Equals(c.Password));
                //var gateway = gatewayList.FirstOrDefault(t => t.UserName.Equals(c.Username) && t.PWD.Equals(c.Password));
                if (gateway != null)
                {
                    c.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
                }
                else
                {
                    c.ReturnCode = MqttConnectReturnCode.ConnectionRefusedIdentifierRejected;
                }
            };
            return options.ConnectionValidator;
        }

        /// <summary>
        /// 保存网关
        /// </summary>
        /// <param name="id"></param>
        public void SaveGateWay2List(MQTTGatewayEntity mQTTGatewayEntity)
        {
            var gateway = gatewayList.FirstOrDefault(t => t.Id.Equals(mQTTGatewayEntity.Id));
            if (gateway != null)
            {
                gateway.ClientId = mQTTGatewayEntity.ClientId;
                gateway.UserName = mQTTGatewayEntity.UserName;
                gateway.PWD = mQTTGatewayEntity.PWD;
                gateway.Topic = mQTTGatewayEntity.Topic;
            }
            else
            {
                gatewayList.Add(mQTTGatewayEntity);
            }
        }

        /// <summary>
        /// 删除网关
        /// </summary>
        /// <param name="id"></param>
        public void DeleteGateWayFromList(String id) 
        {
            gatewayList.RemoveAll(t => t.Id.Equals(id));
            gatewayTagList.RemoveAll(t => t.BindId.Equals(id));
        }

        /// <summary>
        /// 保存Tag
        /// </summary>
        /// <param name="id"></param>
        public void SaveTag2List(MQTTGatewayTagEntity mQTTGatewayTagEntity)
        {
            var tag = gatewayTagList.FirstOrDefault(t => t.Id.Equals(mQTTGatewayTagEntity.Id));
            if (tag != null)
            {
                tag.DeviceCode = mQTTGatewayTagEntity.DeviceCode;
                tag.ParameterCode = mQTTGatewayTagEntity.ParameterCode;
                tag.ParameterName = mQTTGatewayTagEntity.ParameterName;
                tag.MinValue = mQTTGatewayTagEntity.MinValue;
                tag.MaxValue = mQTTGatewayTagEntity.MaxValue;
                tag.warnFlag = mQTTGatewayTagEntity.warnFlag;
                tag.Sort = mQTTGatewayTagEntity.Sort;
            }
            else 
            {
                gatewayTagList.Add(mQTTGatewayTagEntity);
            }
        }

        /// <summary>
        /// 删除Tag
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTagFromList(String id)
        {
            gatewayTagList.RemoveAll(t => t.Id.Equals(id));
        }

        /// <summary>
        /// 检测数值队列，保存到数据库
        /// </summary>
        public void StartValQueue()
        {
            
            ThreadPool.QueueUserWorkItem((a) =>
            {
                StringBuilder sql = new StringBuilder();
                while (true)
                {
                    int queueCount = gateWayValQueue.Count;
                    if (queueCount > 0)
                    {
                        //List<GatewayValEntity> valList = new List<GatewayValEntity>();
                        sql.Clear();
                        int deCount = queueCount > 50 ? 50 : queueCount;
                        while (deCount > 0) 
                        {
                            GatewayValEntity val = gateWayValQueue.Dequeue();//出队
                            //valList.Add(val);
                            sql.Append("insert Gateway_Values(Id,BindId,DeviceCode,ParameterCode,ParameterName,ParameterValue,ParameterType,ParameterCategory,Year,Month,DAY,CreateDate) VALUES('");
                            sql.Append(val.Id + "','");
                            sql.Append(val.BindId + "','");
                            sql.Append(val.DeviceCode + "','");
                            sql.Append(val.ParameterCode + "','");
                            sql.Append(val.ParameterName + "','");
                            sql.Append(val.ParameterValue + "','");
                            sql.Append(val.ParameterType + "','");
                            sql.Append(val.ParameterCategory + "','");
                            sql.Append(((DateTime)val.CreateDate).Year + "','");
                            sql.Append(((DateTime)val.CreateDate).Month + "','");
                            sql.Append(((DateTime)val.CreateDate).Day + "','");
                            sql.Append(((DateTime)val.CreateDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "');");
                            deCount = deCount - 1;
                        }
                        //入库
                       // gatewayValBLL.SaveList(valList);
                        gatewayValBLL.SaveList(sql);
                        
                    }
                    else
                    {
                        //如果队列中没有数据，休眠10秒.
                        Thread.Sleep(10000);
                    }
                }
            });
        }
    }
}
