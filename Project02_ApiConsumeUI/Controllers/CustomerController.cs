using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project02_ApiConsumeUI.DTOS;

namespace Project02_ApiConsumeUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Müşteri listesini getiren metot
        public async Task<IActionResult> CustomerList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5118/api/Customer");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCustomerDTO>>(jsonData);
                return View(values);
            }

            ModelState.AddModelError("", "Müşteri listesi alınırken bir hata oluştu.");
            return View();
        }

        // Yeni müşteri oluşturma formunu gösteren metot
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        // Yeni müşteri ekleme işlemini gerçekleştiren metot
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Lütfen tüm alanları eksiksiz doldurun.");
                return View(model);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(
                jsonData,
                Encoding.UTF8,
                "application/json"
            );

            var responseMessage = await client.PostAsync(
                "http://localhost:5118/api/Customer",
                stringContent
            );

            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("CustomerList");

            ModelState.AddModelError("", "Müşteri oluşturulurken bir hata oluştu.");
            return View(model);
        }

        // Müşteri silme işlemini gerçekleştiren metot
        public async Task<IActionResult> DeleteCustomer(int? id)
        {
            if (id == null)
                return NotFound("ID alanı boş bırakıldı.");

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync(
                $"http://localhost:5118/api/Customer?id={id}"
            );

            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("CustomerList");

            ModelState.AddModelError("", "Müşteri silme işlemi başarısız oldu.");
            return View();
        }

        // Güncellenmek istenen müşteriyi getiren metot
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int? id)
        {
            if (id == null)
                return NotFound("ID alanı boş.");

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(
                $"http://localhost:5118/api/Customer/id?id={id}"
            );

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdCustomerDTO>(jsonData);
                if (values != null)
                {
                    return View(
                        new UpdateCustomerDTO
                        {
                            CustomerId = values.CustomerId,
                            CustomerName = values.CustomerName,
                            CustomerSurName = values.CustomerSurName,
                            CustomerBalance = values.CustomerBalance,
                        }
                    );
                }
            }

            ModelState.AddModelError("", "Müşteri bilgileri alınırken bir hata oluştu.");
            return View();
        }

        // Müşteri güncelleme işlemini gerçekleştiren metot
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDTO updateCustomerDTO)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Lütfen tüm alanları eksiksiz doldurun.");
                return View(updateCustomerDTO);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCustomerDTO);
            StringContent stringContent = new StringContent(
                jsonData,
                Encoding.UTF8,
                "application/json"
            );

            var responseMessage = await client.PutAsync(
                "http://localhost:5118/api/Customer",
                stringContent
            );

            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("CustomerList");

            ModelState.AddModelError("", "Müşteri güncellenirken bir hata oluştu.");
            return View(updateCustomerDTO);
        }
    }
}
