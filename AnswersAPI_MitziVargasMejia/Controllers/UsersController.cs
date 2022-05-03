using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnswersAPI_MitziVargasMejia.Models;
using AnswersAPI_MitziVargasMejia.Attributes;
using AnswersAPI_MitziVargasMejia.Tools;


namespace AnswersAPI_MitziVargasMejia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKey]
    public class UsersController : ControllerBase
    {
        private readonly AnswersDBContext _context;
        private Tools.Crypto MyCrypto { get; set; }

        public UsersController(AnswersDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //validacion para el login
        [HttpGet("ValidateUserLogin")]//definir el nombre del get
        public async Task<ActionResult<User>> ValidateUserLogin(string pEmail, string pPassword)
        {
            try
            {
                //se valida el usuario por email y el password encriptado a nivel de api
                string ApiLevelEncriptedPassword = MyCrypto.EncriptarEnUnSentido(pPassword);

                //TAREA: Hacer esta misma consulta pero usando LINQ
                //var qu = _context.Users.Select(u => u.UserName == pEmail && u.UserPassword == ApiLevelEncriptedPassword);

                /*var query = from u in _context.Users
                            where u.UserName.Equals(pEmail) && u.UserPassword.Equals(ApiLevelEncriptedPassword)
                            select u;

                if (query == null)
                {
                    return NotFound();
                }*/

                /* revisar porque cae*/
                 var user = await _context.Users.SingleOrDefaultAsync(e => e.UserName == pEmail && e.UserPassword == ApiLevelEncriptedPassword);


                //si no hay respuesta en la consulta se devuelve el mensaje hhtp Not found
                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
