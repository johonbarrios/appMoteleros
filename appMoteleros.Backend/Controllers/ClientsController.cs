namespace appMoteleros.Backend.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Backend.Helpers;
    using Backend.Models;
    using Common.Models;
    public class ClientsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // Retorna la lista de todos los clientes ordenada
        public async Task<ActionResult> Index()
        {
            return View(await this.db.Clients.OrderBy(p => p.Description).ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // CREAR CLIENTE
        // ----------------------------------------------------
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClientView view)
        {
            if (ModelState.IsValid)
            {

                var pic = string.Empty;
                var folder = "~/Content/Clients";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var client = this.ToClient(view, pic);
                this.db.Clients.Add(client);
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }

        private Client ToClient(ClientView view, string pic)
        {
            return new Client
            {
                LicensePlate = view.LicensePlate,
                Description = view.Description,
                FirstName = view.FirstName,
                Phone = view.Phone,
                Notes = view.Notes,
                ImagePath = pic,
            };
        }


        // EDITAR CLIENTE
        // ----------------------------------------------------
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            var view = this.ToView(client);
            return View(view);
        }
        private ClientView ToView(Client client)
        {
            return new ClientView
            {
                LicensePlate = client.LicensePlate,
                Description = client.Description,
                FirstName = client.FirstName,
                Phone = client.Phone,
                Notes = client.Notes,
                ImagePath = client.ImagePath,
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClientView view)
        {
            if (ModelState.IsValid)
            {
                var pic = view.ImagePath;
                var folder = "~/Content/Clients";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                }

                var client = this.ToClient(view, pic);
                this.db.Entry(client).State = EntityState.Modified;
                await this.db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(view);
        }



        // BORRAR CLIENTE
        // ----------------------------------------------------
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var client = await db.Clients.FindAsync(id);
            this.db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
