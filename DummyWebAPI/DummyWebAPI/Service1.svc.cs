using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace DummyWebAPI
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Service1
    {
        // HTTP GET を使用するために [WebGet] 属性を追加します (既定の ResponseFormat は WebMessageFormat.Json)。
        // XML を返す操作を作成するには、
        //     [WebGet(ResponseFormat=WebMessageFormat.Xml)] を追加し、
        //     操作本文に次の行を含めます。
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public void DoWork()
        {
            // 操作の実装をここに追加してください
            return;
        }

        // 追加の操作をここに追加して、[OperationContract] とマークしてください
        [WebGet]
        [OperationContract]
        public List<TestData> SendData()
        {
            //WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";

            var list = new List<TestData>();
            for(var i= 0;i< 10; i++)
            {
                list.Add(new TestData("Name" + i.ToString(), 20 + i));
            }

            return list;
        }

        [OperationContract(Action ="POST")]
        public DataCount GetDataCount(List<TestData> list)
        {
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
            
            return new DataCount(list.Count);
        }
    }
}
