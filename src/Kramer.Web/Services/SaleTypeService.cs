using Kramer.Domain;
using Kramer.Repository;
using Kramer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramer.Services
{
    public class SaleTypeService : ISaleTypeService
    {
        private IUserRepository userRepository;
        private ISaleTypeRepository saleTypeRepository;
        private IRoleRepository roleRepository;

        public SaleTypeService(
            IUserRepository userRepository, 
            ISaleTypeRepository saleTypeRepository, 
            IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.saleTypeRepository = saleTypeRepository;
            this.roleRepository = roleRepository;
        }

        public List<SaleType> GetAvailableSaleTypes(string userId)
        {
            if (userRepository.IsAdmin(userId)) //Se for admin, já retorna todos os SaleTypes
                return saleTypeRepository.All().ToList();

            var user = userRepository.GetById(userId); //pego o usuário
            var userRolesIds = user.Roles.Select(_ => _.RoleId).ToList(); //seleciono os Ids das roles desse usuário
            var userRolesNames = userRolesIds.Select(id => roleRepository.GetById(id).Name).ToList() ; //pelos IDs, busco os nomes das roles

            return saleTypeRepository.All().Where(saleType => userRolesNames.Contains(saleType.Name)).ToList(); //retorno todos os saleTypes que o usuário tenha role
        }
    }
}