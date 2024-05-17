using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.ABI.ARC32
{
    public class CallConfigSpec
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CallConfig No_op { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CallConfig Opt_in { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CallConfig Close_out { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CallConfig Update_application { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CallConfig Delete_application { get; set; }

        internal List<string> ToOnCompletionList()
        {
            List<string> list = new List<string>();
            if (No_op == CallConfig.CALL || No_op == CallConfig.ALL)
            {
                list.Add(Core.OnCompleteType.NoOp.ToString());
            }
            if (Opt_in == CallConfig.CALL || Opt_in == CallConfig.ALL)
            {
                list.Add(Core.OnCompleteType.OptIn.ToString());
            }
            if (Close_out == CallConfig.CALL || Close_out == CallConfig.ALL)
            {
                list.Add(Core.OnCompleteType.CloseOut.ToString());
            }
            if (Update_application == CallConfig.CALL || Update_application == CallConfig.ALL)
            {
                list.Add(Core.OnCompleteType.UpdateApplication.ToString());
            }
            if (Delete_application == CallConfig.CALL || Delete_application == CallConfig.ALL)
            {
                list.Add(Core.OnCompleteType.DeleteApplication.ToString());
            }
            return list;
        }
    }
}
