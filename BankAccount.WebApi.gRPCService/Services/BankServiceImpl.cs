//using System.Threading.Tasks;
//using Grpc.Core;
//using BankAccount.Grpc;  // Namespace generated from your .proto file
////using BankAccount.WebAPI.DAL;
//using BankAccount.WebApi.BL;
//using global::Grpc.Core;

//namespace BankAccount.WebApi.gRPCService.Services
//{    

//    public class BankServiceImpl : BankService.BankServiceBase
//    {
//        private readonly CustomerService _customerService;

//        public BankServiceImpl(CustomerService customerService)
//        {
//            _customerService = customerService;
//        }

//        // Implement the GetCustomer RPC
//        public override async Task<CustomerResponse> GetCustomer(CustomerRequest request, ServerCallContext context)
//        {
//            var customer = await _customerService.GetCustomerByIdAsync(request.CustomerId);

//            return new CustomerResponse
//            {
//                CustomerId = customer.CustomerId,
//                FirstName = customer.FirstName,
//                LastName = customer.LastName,
//                Email = customer.Email,
//                PhoneNumber = customer.PhoneNumber
//            };
//        }

//        // Implement the CreateCustomer RPC
//        public override async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest request, ServerCallContext context)
//        {
//            var customer = await _customerService.CreateCustomerAsync(
//                request.FirstName,
//                request.LastName,
//                request.Email,
//                request.PhoneNumber
//            );

//            return new CustomerResponse
//            {
//                CustomerId = customer.CustomerId,
//                FirstName = customer.FirstName,
//                LastName = customer.LastName,
//                Email = customer.Email,
//                PhoneNumber = customer.PhoneNumber
//            };
//        }
//    }

//}
