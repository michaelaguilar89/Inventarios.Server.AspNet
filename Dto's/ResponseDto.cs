namespace Inventarios.Server.AspNet.Dto_s
{
    public class ResponseDto
    {
        public string DisplayMessage { get; set; }

        public string ErrorMessage { get; set; }

        public Object Result { get; set; }

        public bool IsSuccess {get;set;}
        public int previosPage { get; set; }
        public int nextPage { get; set; }
       

    }
}
