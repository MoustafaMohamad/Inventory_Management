using Autofac;
using FluentValidation;
using Inventory_Management.Common;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common.Repositories;
using Inventory_Management.Data;
using Inventory_Management.Entities;
using Inventory_Management.Features.Products.AddProduct;
using Inventory_Management.Features.Products.AddProduct.Commands;
using Inventory_Management.Features.Products.UpdateProduct;
using Inventory_Management.Features.Users.ChangePassword;
using Inventory_Management.Features.Users.ForgetPassword;
using Inventory_Management.Features.Users.Loginuser;
using Inventory_Management.Features.Users.RegisterUser;
using Inventory_Management.Features.Users.ResetPassword;

namespace Common
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<Context>().InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(RequestParameters<>)).InstancePerLifetimeScope();
            builder.RegisterType<UserState>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(RequestParameters<>).Assembly).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<RequestParameters<User>>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RequestParameters<Role>>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RequestParameters<OtpVerification>>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RequestParameters<Product>>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ICloudinaryService).Assembly)
                   .AsImplementedInterfaces()
                   .SingleInstance();
            builder.RegisterType<AddProductValidator>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UpdateProductValidator>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RegisterUserValidator>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<LoginUserValidator>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ForgetPasswordValidator>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ChangePasswordValidator>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ResetpasswordValidator>().AsSelf().InstancePerLifetimeScope();


        }
    }
}
