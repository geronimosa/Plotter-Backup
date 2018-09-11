<%@ Page Language="C#" %>

<%@ import Namespace="System.Net" %>
<%@ import Namespace="System.IO" %>
<%@ import Namespace="System.Text" %>

<script runat="server">
void PostMe(object sender, EventArgs e){
    string poststring = String.Format("tiusername={0}&tipassword={1}", text1.Text, text2.Text);
    ResponseResult.Text = "<hr/>" +
    GetResponseString("https://secure.telkomsa.net/titracker/servlet/LoginServlet", poststring);
}

String GetResponseString(string url, string poststring){
    HttpWebRequest httpRequest =
    (HttpWebRequest)WebRequest.Create("https://secure.telkomsa.net/titracker/servlet/LoginServlet"); 
    
    httpRequest.Method = "POST"; 
    httpRequest.ContentType = "application/x-www-form-urlencoded"; 

    byte[] bytedata =  Encoding.UTF8.GetBytes(poststring);
    httpRequest.ContentLength = bytedata.Length;

    Stream requestStream = httpRequest.GetRequestStream();
    requestStream.Write(bytedata, 0, bytedata.Length);
    requestStream.Close();


    HttpWebResponse httpWebResponse = 
    (HttpWebResponse)httpRequest.GetResponse();
    Stream responseStream =  httpWebResponse.GetResponseStream();

    StringBuilder sb = new StringBuilder();

    using (StreamReader reader = 
    new StreamReader(responseStream, System.Text.Encoding.UTF8))
    {
      string line;
      while ((line = reader.ReadLine()) != null)
      {
        sb.Append(line);
      }
    }

    return sb.ToString();

}

</script>
<html>
<head runat="server">
</head>
<body>
    <form id="Form1" runat="Server">
        field1 : <asp:TextBox runat="Server" id="text1"/>
        <br/>
        field2 : <asp:TextBox runat="Server" id="text2"/>
        <br/>
        <asp:button ID="Button1" runat="Server" onclick="PostMe" Text="Post"/>

        
        <asp:Literal runat="Server" id="ResponseResult" />
        
    </form>
</body>
</html>