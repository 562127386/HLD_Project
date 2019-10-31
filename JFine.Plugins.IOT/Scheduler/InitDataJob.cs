﻿using JFine.Data.Repository;
using JFine.Job;
using JFine.Plugins.IOT.IOTHubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JFine.Common.Json;
/********************************************************************************
**文 件 名:CheckDeviceStatusJob
**命名空间:JFine.Plugins.IOT.Scheduler
**描    述:初始化应用数据
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2019-07-26 18:18:29
**修 改 人:
**修改日期:
**修改描述:
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JFine.Plugins.IOT.Domain.Models.MQTT;
using JFine.Plugins.IOT.Busines.MQTT;
using JFine.Cache.Redis;

namespace JFine.Plugins.IOT.Scheduler
{
    public class InitDataJob : ITaskJob
    {

        public bool CloseJob(JFine.Domain.Models.SysCommon.Job_ScheduleEntity jobModel)
        {
            throw new NotImplementedException();
        }

        public bool RunJob(Dictionary<string, string> dic_paras, string jobName, string id)
        {
            /*
             * 初始化应用数据
             * 1、加载采集网管的参数配置到Redis
             * 2、加载redis缓存的当前处理数据到系统内
             */
            RedisHelper redisHelper = new RedisHelper(IOTConstant.MQTT_REDIS_DB_INDEX);
            //1、加载采集网管的参数配置到Redis
            if(!(redisHelper.KeyExists(IOTConstant.MQTT_GATEWAY_KEY) && redisHelper.KeyExists(IOTConstant.MQTT_GATEWAY_TAG_KEY)))
            {
                List<MQTTGatewayEntity> gatewayList = new List<MQTTGatewayEntity>();
                List<MQTTGatewayTagEntity> gatewayTagList = new List<MQTTGatewayTagEntity>();
                MQTTGatewayBLL mQTTGatewayBll = new MQTTGatewayBLL();
                MQTTGatewayTagBLL mQTTGatewayTagBll = new MQTTGatewayTagBLL();
                gatewayList = mQTTGatewayBll.GetList().ToList();
                gatewayTagList = mQTTGatewayTagBll.GetList().ToList();
                redisHelper.ListRightPush<MQTTGatewayEntity>(IOTConstant.MQTT_GATEWAY_KEY, gatewayList);
                redisHelper.ListRightPush<MQTTGatewayTagEntity>(IOTConstant.MQTT_GATEWAY_TAG_KEY, gatewayTagList);
            }
            //2、加载redis缓存的当前处理数据到系统内
            if (redisHelper.KeyExists(IOTConstant.MQTT_CURRENTVALUE_KEY))
            {
                IEnumerable<string> keys = redisHelper.HashKeys(IOTConstant.MQTT_CURRENTVALUE_KEY);
                foreach (var item in keys)
                {
                    var value = redisHelper.HashGet(IOTConstant.MQTT_CURRENTVALUE_KEY, item);
                     ModbusHub.modbusData[item] = value;
                }
            }
            
            return true;
        }

        public bool RunJobBefore(JFine.Domain.Models.SysCommon.Job_ScheduleEntity jobModel)
        {
            throw new NotImplementedException();
        }
    }
}
