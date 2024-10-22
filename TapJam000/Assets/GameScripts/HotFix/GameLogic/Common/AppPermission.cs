using UnityEngine;
using UnityEngine.Android;

namespace GameLogic
{
    public static class AppPermission
    {
        private static PermissionCallbacks m_PermissionCallbacks;
        private static bool m_IsGetAllPermission;

        /// <summary>
        /// ������Ȩ��
        /// </summary>
        public static void RequestPermissions()
        {
            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) &&
                Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)) return;

            m_IsGetAllPermission = false;

            // ����ص�
            m_PermissionCallbacks = new PermissionCallbacks();
            m_PermissionCallbacks.PermissionDenied += OnPermissionDenied;
            m_PermissionCallbacks.PermissionGranted += OnPermissionGranted;
            m_PermissionCallbacks.PermissionDeniedAndDontAskAgain += OnPermissionDeniedAndDontAskAgain;

            // ����Ȩ����
            string[] permissions =
            {
                Permission.ExternalStorageRead,
                Permission.ExternalStorageWrite
            };

            // ִ��������Ȩ��
            Permission.RequestUserPermissions(permissions, m_PermissionCallbacks);
        }

        /// <summary>
        /// ����Ȩ�ޱ��ܾ�
        /// </summary>
        /// <param name="permission"></param>
        private static void OnPermissionDenied(string permission)
        {
            Debug.Log("Ȩ�����뱻�ܾ�");
        }

        /// <summary>
        /// ����Ȩ�޳ɹ�
        /// </summary>
        /// <param name="permission"></param>
        private static void OnPermissionGranted(string permission)
        {
            // ���Ȩ���Ƿ�ȫ��ͨ��
            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead) &&
                Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                // ÿ��Ȩ��ͨ������ص���������ֹ�ظ��ص�
                if (!m_IsGetAllPermission)
                {
                    m_IsGetAllPermission = true;

                    // �����ﴦ��Ȩ��ͨ�����߼�
                    // do something

                    Debug.Log("Ȩ������ͨ��");
                }
            }
        }

        /// <summary>
        /// ����Ȩ�ޱ��ܾ�,�Ҳ���ѯ��
        /// </summary>
        /// <param name="permission"></param>
        private static void OnPermissionDeniedAndDontAskAgain(string permission)
        {
            Debug.Log("Ȩ�����뱻�ܾ��Ҳ���ѯ��");
        }
    }
}