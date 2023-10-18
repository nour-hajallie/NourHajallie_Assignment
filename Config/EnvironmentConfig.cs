using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NourHajallie_AutomationProject.Config
{
    public class EnvironmentConfig
    {

        private String sampleAppUrl;
        private String ajaxUrl;
        private String textInputUrl;

        public EnvironmentConfig()
        {
            sampleAppUrl = "http://www.uitestingplayground.com/sampleapp";
            ajaxUrl = "http://www.uitestingplayground.com/ajax";
            textInputUrl = "http://www.uitestingplayground.com/textinput";

            setSampleAppUrl(sampleAppUrl);
            setAjaxUrl(ajaxUrl);
            setTextInputUrl(textInputUrl);
        }

        public void setSampleAppUrl(String _sampleAppUrl)
        {
            sampleAppUrl = _sampleAppUrl;

        }

        public void setAjaxUrl(String _ajaxUrl)
        {
            ajaxUrl = _ajaxUrl;

        }

        public void setTextInputUrl(String _textInputUrl)
        {
            textInputUrl = _textInputUrl;

        }

        public String getSampleAppUrl()
        {
            return sampleAppUrl;
        }

        public String getAjaxUrl ()
        {
            return ajaxUrl;
        }

        public String getTextInputUrl()
        {
            return textInputUrl;
        }
    }
}
