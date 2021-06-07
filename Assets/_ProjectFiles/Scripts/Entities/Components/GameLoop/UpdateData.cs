namespace Alive
{
    /// <summary>
    /// Информация об игровом цикле.
    /// </summary>
    public readonly struct UpdateData
    {
        /// <summary>
        /// Пройденное время с последнего обновления.
        /// </summary>
        public readonly float deltaTime;

        public UpdateData(float deltaTime)
        {
            this.deltaTime = deltaTime;
        }
    }
}