namespace GDLibrary
{
    /// <summary>
    /// Provides an interface for every object that reacts to the color switch
    /// </summary>
    public interface IInteractable
    {

        /// <summary>
        /// Manages how the switch will affect the object
        /// </summary>
        /// <example>
        ///     void Switch(bool isRed){
        ///         if(isRed != this.isRed)
        ///             this.color.A = 0.5f;
        ///         else
        ///             this.color.A = 1f;
        ///     }
        /// </example>
        void Switch();
    }
}
