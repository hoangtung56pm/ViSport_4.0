using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JobExecutor
/// </summary>
///  [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name = "JobExecutorSoap", Namespace = "http://tempuri.org/")]
public class JobExecutor : System.Web.Services.Protocols.SoapHttpClientProtocol
{
    private System.Threading.SendOrPostCallback ExecuteOperationCompleted;

    /// <remarks/>
    public JobExecutor()
    {
        this.Url = "http://localhost:2589/S2Jobs/JobExecutor.asmx";
    }

    public JobExecutor(string UrlEndpoint)
    {
        this.Url = UrlEndpoint;
    }

    /// <remarks/>
    public event ExecuteCompletedEventHandler ExecuteCompleted;

    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Execute", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    public int Execute(int jobID)
    {
        object[] results = this.Invoke("Execute", new object[] {
                    jobID});
        return ((int)(results[0]));
    }

    /// <remarks/>
    public System.IAsyncResult BeginExecute(int jobID, System.AsyncCallback callback, object asyncState)
    {
        return this.BeginInvoke("Execute", new object[] {
                    jobID}, callback, asyncState);
    }

    /// <remarks/>
    public int EndExecute(System.IAsyncResult asyncResult)
    {
        object[] results = this.EndInvoke(asyncResult);
        return ((int)(results[0]));
    }

    /// <remarks/>
    public void ExecuteAsync(int jobID)
    {
        this.ExecuteAsync(jobID, null);
    }

    /// <remarks/>
    public void ExecuteAsync(int jobID, object userState)
    {
        if ((this.ExecuteOperationCompleted == null))
        {
            this.ExecuteOperationCompleted = new System.Threading.SendOrPostCallback(this.OnExecuteOperationCompleted);
        }
        this.InvokeAsync("Execute", new object[] {
                    jobID}, this.ExecuteOperationCompleted, userState);
    }

    private void OnExecuteOperationCompleted(object arg)
    {
        if ((this.ExecuteCompleted != null))
        {
            System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
            this.ExecuteCompleted(this, new ExecuteCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
        }
    }

    /// <remarks/>
    public new void CancelAsync(object userState)
    {
        base.CancelAsync(userState);
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
public delegate void ExecuteCompletedEventHandler(object sender, ExecuteCompletedEventArgs e);

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.1")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ExecuteCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
{

    private object[] results;

    internal ExecuteCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
        base(exception, cancelled, userState)
    {
        this.results = results;
    }

    /// <remarks/>
    public int Result
    {
        get
        {
            this.RaiseExceptionIfNecessary();
            return ((int)(this.results[0]));
        }
    }
}
