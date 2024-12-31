using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Domain.Base.Dto
{
    public class ApiResponse<T>
    {
        public bool IsValidatableResponse {  get; set; }
        public T Data { get; set; }
        public List<string>? Messages {  get; set; }

        public void CreateSuccessResponse(T data,string successMessage)
        {
            IsValidatableResponse = true;
            Data = data;
            Messages = new List<string>() { successMessage};
        }

        public void CreateFailedResponse(T data, List<string> errorMessages)
        {
            IsValidatableResponse = false;
            Data = data;
            Messages = new List<string>();
            foreach (string message in errorMessages) { 
                Messages.Add(message);
            }
        }
    }
}
