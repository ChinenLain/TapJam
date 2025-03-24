using UnityEngine;
using UnityEngine.Android;

namespace GameLogic
{
    public static class AppPermission
    {
        private static PermissionCallbacks m_PermissionCallbacks;
        private static bool m_IsGetAllPermission;

        /// <summary>
        /// 申请多个权限
        /// </summary>
        public static void RequestPermissions()
        {
            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) &&
                Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)) return;

            m_IsGetAllPermission = false;

            // 申请回调
            m_PermissionCallbacks = new PermissionCallbacks();
            m_PermissionCallbacks.PermissionDenied += OnPermissionDenied;
            m_PermissionCallbacks.PermissionGranted += OnPermissionGranted;
            m_PermissionCallbacks.PermissionDeniedAndDontAskAgain += OnPermissionDeniedAndDontAskAgain;

            // 申请权限组
            string[] permissions =
            {
                Permission.ExternalStorageRead,
                Permission.ExternalStorageWrite
            };

            // 执行申请多个权限
            Permission.RequestUserPermissions(permissions, m_PermissionCallbacks);
        }

        /// <summary>
        /// 申请权限被拒绝
        /// </summary>
        /// <param name="permission"></param>
        private static void OnPermissionDenied(string permission)
        {
            Debug.Log("权限申请被拒绝");
        }

        /// <summary>
        /// 申请权限成功
        /// </summary>
        /// <param name="permission"></param>
        private static void OnPermissionGranted(string permission)
        {
            // 检查权限是否全部通过
            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) &&
                Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                // 每个权限通过都会回调，变量防止重复回调
                if (!m_IsGetAllPermission)
                {
                    m_IsGetAllPermission = true;

                    // 在这里处理权限通过的逻辑
                    // do something

                    Debug.Log("权限申请通过");
                }
            }
        }

        /// <summary>
        /// 申请权限被拒绝,且不再询问
        /// </summary>
        /// <param name="permission"></param>
        private static void OnPermissionDeniedAndDontAskAgain(string permission)
        {
            Debug.Log("权限申请被拒绝且不再询问");
        }
    }
}