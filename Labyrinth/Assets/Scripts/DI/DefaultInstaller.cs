using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<PlayerControl>().AsSingle();
    }
}
