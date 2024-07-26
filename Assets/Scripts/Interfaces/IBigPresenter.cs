using System.Collections.Generic;


namespace Lessons.Architecture.PM
{
    public interface IBigPresenter
    {
        IReadOnlyList<ISmallPresenter> SmallPresenters { get; }
   
    }

}
