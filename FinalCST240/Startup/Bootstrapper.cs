using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace FinalCST240.Startup
{
    public class Bootstrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ShoppingCart>().As<IShoppingCart>();
            builder.RegisterType<ShoppingCartListCarts>().As<IShoppingCartListCarts>();
            return builder.Build();


        }

    }
}
