using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUPMS.Infrastructure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum MessageType
    {
        Info,
        Warning,
        Error
    }
    /// <summary>
    /// 
    /// </summary>
    public enum EncodingType
    {
        ASCII,
        Unicode,
        UTF7,
        UTF8
    }
    /// <summary>
    /// 
    /// </summary>
    public enum Response
    {
        Success = 0,
        Failure = 1,
        Information = 2,
        Referenced = 3,
    }
    /// <summary>
    /// 
    /// </summary>
    public enum ActionType
    {
        Add = 0,
        Edit = 1,
        Delete = 2,
        Fetch = 3,
    }
    /// <summary>
    /// 
    /// </summary>
    public enum AlertType
    {
        Success,
        Info,
        Warning,
        Error
    }
    /// <summary>
    /// 
    /// </summary>
    public enum Modules
    {
        Users = 301,
        Language = 202,
        askStatus = 1,
        Roles = 303,
        UsersGroup = 304,
        EventTypes = 112
    }
}
