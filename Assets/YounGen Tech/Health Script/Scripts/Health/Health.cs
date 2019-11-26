using UnityEngine;
using UnityEngine.Events;
using System;

namespace YounGenTech.HealthScript {
    [AddComponentMenu("YounGen Tech/Health/Health"), DisallowMultipleComponent]
    public class Health : MonoBehaviour, IStat {
#if UNITY_EDITOR
        float editor_value;
        float editor_maxValue;
#endif

        [SerializeField, Tooltip("Current health value")]
        float _value = 100;

        [SerializeField, Tooltip("Max health value")]
        float _maxValue = 100;

        [SerializeField, Tooltip("Health value will not go above the max value and won't go below 0"), UnityEngine.Serialization.FormerlySerializedAs("capHealth")]
        bool _clampHealth = true;

        [SerializeField, Tooltip("Disables the health value from being changed"), UnityEngine.Serialization.FormerlySerializedAs("disableHealthChange")]
        bool _disableHealthChange = false;

        [SerializeField, Tooltip("Disable damage")]
        bool _invincible;

        [SerializeField, Tooltip("Disable healing")]
        bool _incurable;

        /// <summary>Health value has changed.</summary>
        public HealthChangeEvent OnChangedHealth;

        /// <summary>Health max value has changed.</summary>
        public HealthChangeEvent OnChangedMaxHealth;

        #region Received Health Events
        /// <summary>Health value went up after previously being at or below zero.</summary>
        public UnityHealthEvent OnRevived;

        /// <summary>Health value went down after previously being at or above max value.</summary>
        public UnityHealthEvent OnFirstDamaged;

        /// <summary>Health value went up.</summary>
        public UnityHealthEvent OnHeal;

        /// <summary>Health value went down.</summary>
        public UnityHealthEvent OnDamaged;

        /// <summary>Health value is at or above max value.</summary>
        public UnityHealthEvent OnFullyRestored;

        /// <summary>Health value is at or below zero.</summary>
        public UnityHealthEvent OnDeath;
        #endregion

        #region Caused Health Events
        /// <summary>Caused another object's health value to go up after previously being at or below zero.</summary>
        public UnityHealthEvent OnCausedRevival;

        /// <summary>Caused another object's health value to go down after previously being at or above max value.</summary>
        public UnityHealthEvent OnCausedFirstDamage;

        /// <summary>Caused another object's health value to go up.</summary>
        public UnityHealthEvent OnCausedHeal;

        /// <summary>Caused another object's health value to go down.</summary>
        public UnityHealthEvent OnCausedDamage;

        /// <summary>Caused another object's health value to reach max value.</summary>
        public UnityHealthEvent OnCausedFullRestoration;

        /// <summary>Caused another object's health value to reach or go below zero.</summary>
        public UnityHealthEvent OnCausedDeath;
        #endregion

        #region Properties
        /// <summary>Health value will not go above the max value and won't go below 0.</summary>
        public bool ClampHealth {
            get { return _clampHealth; }
            set { _clampHealth = value; }
        }

        /// <summary>Disables the health value from being changed.</summary>
        public bool DisableHealthChange {
            get { return _disableHealthChange; }
            set { _disableHealthChange = value; }
        }

        /// <summary>Disable healing.</summary>
        public bool Incurable {
            get { return _incurable; }
            set { _incurable = value; }
        }

        /// <summary>Disable damage.</summary>
        public bool Invincible {
            get { return _invincible; }
            set { _invincible = value; }
        }

        /// <summary>Max health value.</summary>
        public float MaxValue {
            get { return _maxValue; }
            set {
                float oldValue = MaxValue;

                _maxValue = Mathf.Max(value, 0);

                if(!Mathf.Approximately(MaxValue, oldValue)) {
                    if(OnChangedMaxHealth != null) OnChangedMaxHealth.Invoke(MaxValue);

                    if(ClampHealth)
                        Value = Mathf.Clamp(Value, 0, MaxValue);
                }
            }
        }

        /// <summary>NormalizedValue that will give you a value from 0 to 1 based on a scale from 0 to the max health value.</summary>
        public float NormalizedValue {
            get { return Mathf.InverseLerp(0, MaxValue, Value); }
        }

        /// <summary>Current health value.</summary>
        public float Value {
            get { return _value; }
            set {
                if(DisableHealthChange) return;

                float oldValue = Value;

                _value = ClampHealth ? Mathf.Clamp(value, 0, MaxValue) : value;

                if(!Mathf.Approximately(Value, oldValue))
                    if(OnChangedHealth != null) OnChangedHealth.Invoke(Value);
            }
        }
        #endregion

        protected virtual void Awake() {
            if(OnChangedHealth != null) OnChangedHealth.Invoke(Value);
            if(OnChangedMaxHealth != null) OnChangedMaxHealth.Invoke(MaxValue);
        }

        /// <summary>Directly modify the health value and send events.</summary>
        public float ChangeHealth(HealthEvent healthEvent) {
            if(!DisableHealthChange) {
                float newHealth = Mathf.Clamp(Value + healthEvent.Amount, 0, MaxValue);
                float healthChange = newHealth - Value;

                if(healthChange > 0) {
                    if(Incurable) return Value;
                }
                else if(healthChange < 0) {
                    if(Invincible) return Value;
                }
                else return Value;

                float valueBeforeChange = Value;

                Value = newHealth;

                HealthEvent received = new HealthEvent(healthEvent.EventObject, healthChange);
                HealthEvent caused = new HealthEvent(gameObject, healthChange);

                if(healthChange > 0 && valueBeforeChange <= 0) { //Revived
                    if(OnRevived != null) OnRevived.Invoke(received);

                    if(healthEvent.EventObject) {
                        Health causedEvent = healthEvent.EventObject.GetComponentInParent<Health>();

                        if(causedEvent && causedEvent.OnCausedRevival != null)
                            causedEvent.OnCausedRevival.Invoke(caused);
                    }
                }
                else if(healthChange < 0 && valueBeforeChange >= MaxValue) { //First Damaged
                    if(OnFirstDamaged != null) OnFirstDamaged.Invoke(received);

                    if(healthEvent.EventObject) {
                        Health causedEvent = healthEvent.EventObject.GetComponentInParent<Health>();

                        if(causedEvent && causedEvent.OnCausedFirstDamage != null)
                            causedEvent.OnCausedFirstDamage.Invoke(caused);
                    }
                }

                if(healthChange > 0) { //Heal
                    bool healthIsFull = Value >= MaxValue;

                    if(OnHeal != null) OnHeal.Invoke(received);

                    if(healthEvent.EventObject) {
                        Health causedEvent = healthEvent.EventObject.GetComponentInParent<Health>();

                        if(causedEvent && causedEvent.OnCausedHeal != null)
                            causedEvent.OnCausedHeal.Invoke(caused);
                    }
                }
                else if(healthChange < 0) { //Damaged
                    if(OnDamaged != null) OnDamaged.Invoke(received);

                    if(healthEvent.EventObject) {
                        Health causedEvent = healthEvent.EventObject.GetComponentInParent<Health>();

                        if(causedEvent && causedEvent.OnCausedDamage != null)
                            causedEvent.OnCausedDamage.Invoke(caused);
                    }
                }

                if(Value <= 0) {
                    if(healthChange < 0) { //Death
                        if(OnDeath != null) OnDeath.Invoke(received);

                        if(healthEvent.EventObject) {
                            Health causedEvent = healthEvent.EventObject.GetComponentInParent<Health>();

                            if(causedEvent && causedEvent.OnCausedDeath != null)
                                causedEvent.OnCausedDeath.Invoke(caused);
                        }
                    }
                }
                else if(Value >= MaxValue) { //Restored
                    if(healthChange > 0) {
                        if(OnFullyRestored != null) OnFullyRestored.Invoke(received);

                        if(healthEvent.EventObject) {
                            Health causedEvent = healthEvent.EventObject.GetComponentInParent<Health>();

                            if(causedEvent && causedEvent.OnCausedFullRestoration != null)
                                causedEvent.OnCausedFullRestoration.Invoke(caused);
                        }
                    }
                }
            }

            return Value;
        }

        /// <summary>Subtracts health.</summary>
        public void Damage(HealthEvent healthEvent) {
            if(!DisableHealthChange) {
                healthEvent.Amount = -Mathf.Abs(healthEvent.Amount);
                ChangeHealth(healthEvent);
            }
        }

        /// <summary>Adds health.</summary>
        public void Heal(HealthEvent healthEvent) {
            if(!DisableHealthChange) {
                healthEvent.Amount = Mathf.Abs(healthEvent.Amount);
                ChangeHealth(healthEvent);
            }
        }

        /// <summary>Resets health back to max value.</summary>
        public void Reset() {
            ChangeHealth(new HealthEvent(null, MaxValue));
        }

#if UNITY_EDITOR
        void OnValidate() {
            if(editor_value != Value)
                if(OnChangedHealth != null) OnChangedHealth.Invoke(Value);

            if(editor_maxValue != MaxValue)
                if(OnChangedMaxHealth != null) OnChangedMaxHealth.Invoke(MaxValue);

            Value = _value;
            MaxValue = _maxValue;

            editor_value = Value;
            editor_maxValue = MaxValue;
        }
#endif

        [Serializable]
        public class HealthChangeEvent : UnityEvent<float> { }

        [Serializable]
        public class UnityHealthEvent : UnityEvent<HealthEvent> { }
    }

    [Serializable]
    public struct HealthEvent {

        [SerializeField, Tooltip("The GameObject that caused the health event to happen")]
        GameObject _eventObject;

        [SerializeField, Tooltip("The amount of health to change")]
        float _amount;

        #region Properties
        /// <summary>The amount of health to change.</summary>
        public float Amount {
            get { return _amount; }
            set { _amount = value; }
        }

        /// <summary>The GameObject that caused the health event to happen.</summary>
        public GameObject EventObject {
            get { return _eventObject; }
            set { _eventObject = value; }
        }
        #endregion

        public HealthEvent(GameObject eventObject, float amount) {
            _eventObject = eventObject;
            _amount = amount;
        }
    }
}