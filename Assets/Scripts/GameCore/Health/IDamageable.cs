namespace GameCore.Health
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
        void TakeHeal(float value);
    }
}