using System.Collections.Generic;
using System.Linq;
using AtomicFramework.AtomicObject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Frameworks_And_Libraries.Atomic_Framework.Atomic_Behaviour
{
    public class AtomicBehaviour : AtomicObject
    {
        [Title("Logic"), PropertySpace, PropertyOrder(150)]
        [ShowInInspector, HideInEditorMode]
        private HashSet<ILogic> _logicSet = null;
        
        [ShowInInspector, HideInEditorMode, PropertyOrder(150)]
        private Dictionary<string, ILogic> _logicMap = null;

        private List<IEnable> _enables = null;
        private List<IDisable> _disables = null;
        private List<IUpdate> _updates = null;
        private List<IFixedUpdate> _fixedUpdates = null;
        private List<ILateUpdate> _lateUpdates = null;

        public override void Initialize()
        {
            base.Initialize();
            _logicSet = new HashSet<ILogic>();
            _logicMap = new Dictionary<string, ILogic>();

            _enables = new List<IEnable>();
            _disables = new List<IDisable>();

            _updates = new List<IUpdate>();
            _fixedUpdates = new List<IFixedUpdate>();
            _lateUpdates = new List<ILateUpdate>();
        }

        public bool AddLogic(string key, ILogic target)
        {
            return _logicMap.TryAdd(key, target) && AddLogic(target);
        }

        public bool AddLogic(ILogic target)
        {
            if (target == null)
            {
                return false;
            }

            if (!_logicSet.Add(target))
            {
                return false;
            }

            switch (target)
            {
                case IEnable enable:
                {
                    _enables.Add(enable);

                    if (enabled)
                    {
                        enable.Enable();
                    }

                    break;
                }
                case IDisable disable:
                    _disables.Add(disable);
                    break;
                case IUpdate update:
                    _updates.Add(update);
                    break;
                case IFixedUpdate fixedUpdate:
                    _fixedUpdates.Add(fixedUpdate);
                    break;
                case ILateUpdate lateUpdate:
                    _lateUpdates.Add(lateUpdate);
                    break;
            }

            return true;
        }
        
        public bool RemoveLogic(string key)
        {
            return _logicMap.Remove(key, out var target) && RemoveLogic(target);
        }

        public bool RemoveLogic(ILogic target)
        {
            if (target == null)
            {
                return false;
            }
            
            if (!_logicSet.Remove(target))
            {
                return false;
            }

            switch (target)
            {
                case IEnable enable:
                    _enables.Remove(enable);
                    break;
                case IUpdate tickable:
                    _updates.Remove(tickable);
                    break;
                case IFixedUpdate fixedTickable:
                    _fixedUpdates.Remove(fixedTickable);
                    break;
                case ILateUpdate lateTickable:
                    _lateUpdates.Remove(lateTickable);
                    break;
                case IDisable disable:
                {
                    if (enabled)
                    {
                        disable.Disable();
                    }

                    break;
                }
            }

            return true;
        }

        public void AddLogics(IEnumerable<ILogic> targets)
        {
            foreach (var target in targets)
            {
                AddLogic(target);
            }
        }

        public void RemoveLogics(IEnumerable<ILogic> targets)
        {
            foreach (var target in targets)
            {
                RemoveLogic(target);
            }
        }

        public bool FindLogic<T>(out T result) where T : ILogic
        {
            foreach (var element in _logicSet)
            {
                if (element is not T tElement) continue;
                result = tElement;
                return true;
            }

            result = default;
            return false;
        }

        public bool RemoveLogic<T>() where T : ILogic
        {
            foreach (var element in _logicSet.Where(element => element is T))
            {
                RemoveLogic(element);
                return true;
            }

            return false;
        }

        protected virtual void OnEnable()
        {
            for (int i = 0, count = _enables.Count; i < count; i++)
            {
                var enable = _enables[i];
                enable.Enable();
            }
        }

        protected virtual void OnDisable()
        {
            for (int i = 0, count = _disables.Count; i < count; i++)
            {
                var disable = _disables[i];
                disable.Disable();
            }
        }

        protected virtual void Update()
        {
            var deltaTime = Time.deltaTime;
            
            for (int i = 0, count = _updates.Count; i < count; i++)
            {
                var update = _updates[i];
                update.OnUpdate(deltaTime);
            }
        }

        protected virtual void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            
            for (int i = 0, count = _fixedUpdates.Count; i < count; i++)
            {
                var fixedUpdate = _fixedUpdates[i];
                fixedUpdate.OnFixedUpdate(deltaTime);
            }
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;

            for (int i = 0, count = _lateUpdates.Count; i < count; i++)
            {
                var lateUpdate = _lateUpdates[i];
                lateUpdate.OnLateUpdate(deltaTime);
            }
        }
    }
}