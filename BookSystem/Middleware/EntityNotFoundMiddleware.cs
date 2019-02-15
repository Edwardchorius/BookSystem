using BookSystem.ServiceLayer.Data.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSystem.Middleware
{
    public class EntityNotFoundMiddleware
    {
        private readonly RequestDelegate _next;
        
        public EntityNotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                if (context.Response.StatusCode == 404)
                {
                    context.Response.Redirect("/404");
                }
            }
            catch (EntityNotFoundException)
            {
                context.Response.Redirect("/404");
            }
            catch (EntityAlreadyExistsException)
            {
                context.Response.Redirect("/404");
            }
            catch (UserNotFoundException)
            {
                context.Response.Redirect("/404");
            }
            catch (ArgumentNullException)
            {
                context.Response.Redirect("404");
            }
            catch (ArgumentException)
            {
                context.Response.Redirect("/404");
            }
            catch (NullReferenceException)
            {
                context.Response.Redirect("/404");
            }
            catch (Exception)
            {
                context.Response.Redirect("/404");
            }
        }
    }
}
