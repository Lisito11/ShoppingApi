using System;
using AutoMapper;
using ShoppingAPI.DTOs.Brand;
using ShoppingAPI.DTOs.Product;
using ShoppingAPI.DTOs.ProductBrand;
using ShoppingAPI.DTOs.Role;
using ShoppingAPI.DTOs.ShoppingDetailList;
using ShoppingAPI.DTOs.ShoppingList;
using ShoppingAPI.DTOs.SuperMarket;
using ShoppingAPI.DTOs.SuperMarketProductBrand;
using ShoppingAPI.DTOs.User;
using ShoppingAPI.Models;

namespace ShoppingAPI.Helpers
{
	public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductCreationDTO, Product>();
            CreateMap<Product, InfoProductDTO>();

            CreateMap<Brand, BrandDTO>();
            CreateMap<BrandCreationDTO, Brand>();
            CreateMap<Brand, InfoBrandDTO>();

            CreateMap<ProductBrand, ProductBrandDTO>();
            CreateMap<ProductBrandCreationDTO, ProductBrand>();
            CreateMap<ProductBrand, InfoProductBrandDTO>();

            CreateMap<SuperMarket, SuperMarketDTO>();
            CreateMap<SuperMarketCreationDTO, SuperMarket>();
            CreateMap<SuperMarket, InfoSuperMarketDTO>();

            CreateMap<SuperMarketProductBrand, SuperMarketProductBrandDTO>();
            CreateMap<SuperMarketProductBrandCreationDTO, SuperMarketProductBrand>();
            CreateMap<SuperMarketProductBrand, InfoSuperMarketProductBrandDTO>();

            CreateMap<ShoppingList, ShoppingListDTO>();
            CreateMap<ShoppingListCreationDTO, ShoppingList>();
            CreateMap<ShoppingList, InfoShoppingListDTO>();

            CreateMap<ShoppingDetailList, ShoppingDetailListDTO>();
            CreateMap<ShoppingDetailListCreationDTO, ShoppingDetailList>();
            CreateMap<ShoppingDetailList, InfoShoppingDetailListDTO>();

            CreateMap<Role, RoleDTO>();
            CreateMap<RoleCreationDTO, Role>();
            CreateMap<Role, InfoRoleDTO>();

            CreateMap<User, UserDTO>();
            CreateMap<UserCreationDTO, User>();
            CreateMap<User, InfoUserDTO>();
        }
    }
}

