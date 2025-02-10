using UnityEngine;
using Zenject;

public class UI_Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UI_Manager>().FromComponentInHierarchy().AsSingle();
    }
}