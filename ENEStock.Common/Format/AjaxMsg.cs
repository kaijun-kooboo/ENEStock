using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENEStock.Common.Format
{
    /// <summary>
    /// Ajax 统一消息格式（用来生成 统一的 json 字符串）
    /// </summary>
    public class AjaxMsg
    {
        public AjaxMsgStatu Statu;
        public string Msg;
        public string BackUrl;
        public object Data;
    }

    /// <summary>
    /// Ajax 消息 状态
    /// </summary>
    public enum AjaxMsgStatu
    {
        OK = 1,
        NoOk = 2,
        Err = 3,
        NoLogin = 4,//没有登录
        NoPer = 5//没有权限
    }
}
