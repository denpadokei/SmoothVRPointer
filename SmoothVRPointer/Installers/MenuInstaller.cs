using SmoothVRPointer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace SmoothVRPointer.Installers
{
    internal class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<SettngTabView>().FromNewComponentAsViewController().AsSingle();
        }
    }
}
