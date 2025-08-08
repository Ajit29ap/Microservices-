using Microsoft.AspNetCore.Authorization;

namespace MicroService
{
    public static class Handler
    {
      //  [Authorize(Roles = "getcase")]

        [Authorize(Roles = "app.read")]

        public static IResult EDRSCaseCleanup()
        {
            return Results.Ok("EDRS Cleanup Complete.");
        }

        [Authorize(Roles = "Adduser")]
        public static IResult EdrsAdduser()
        {
            return Results.Ok("Uaser details has been succesfully added");
        }
    }
}
