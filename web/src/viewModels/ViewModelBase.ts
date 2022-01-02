
/**
 * Abstract base class for view models with the type T as the underlying
 * type instance.
 */
export abstract class ViewModelBase<T>
{
    /**
     * Creates an instance of the class with the underlying model.
     * @param model The underlying model wrapped by the view model.
     */
    constructor(public model: T)
    {

    }
}