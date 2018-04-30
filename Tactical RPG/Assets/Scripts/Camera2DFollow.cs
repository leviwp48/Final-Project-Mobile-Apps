using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public Transform target2;
        public Transform target3;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;

        private Shoot ShootScript;

        // Use this for initialization
        private void Start()
        {
            ShootScript = GameObject.Find("Main Camera").GetComponent<Shoot>();
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            if (ShootScript.isThrown)
            {
				
				m_LastTargetPosition = target3.position;
                m_OffsetZ = (transform.position - target3.position).z;

                // only update lookahead pos if accelerating or changed direction
                float xMoveDelta = (target3.position - m_LastTargetPosition).x;

                bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

                if (updateLookAheadTarget)
                {
                    m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
                }
                else
                {
                    m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
                }

				Vector3 underPos = new Vector3 (target3.position.x, target3.position.y - 9f, target3.position.z);
				Vector3 aheadTargetPos = underPos + m_LookAheadPos + Vector3.forward * m_OffsetZ;
                Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

                transform.position = newPos;

                m_LastTargetPosition = target3.position;


            }
            else if (GameManager.instance.p1Turn && ShootScript.isThrown == false)
            {
				m_LastTargetPosition = target.position;
                m_OffsetZ = (transform.position - target.position).z;

                // only update lookahead pos if accelerating or changed direction
                float xMoveDelta = (target.position - m_LastTargetPosition).x;

                bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

                if (updateLookAheadTarget)
                {
                    m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
                }
                else
                {
                    m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
                }

				Vector3 underPos = new Vector3 (target.position.x, target.position.y - 9f, target.position.z);
				Vector3 aheadTargetPos = underPos + m_LookAheadPos + Vector3.forward * m_OffsetZ;
                Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

                transform.position = newPos;

                m_LastTargetPosition = target.position;

            }
            else if(GameManager.instance.p2Turn && ShootScript.isThrown == false)
            {
				m_LastTargetPosition = target2.position;
                m_OffsetZ = (transform.position - target.position).z;

                // only update lookahead pos if accelerating or changed direction
                float xMoveDelta = (target2.position - m_LastTargetPosition).x;

                bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

                if (updateLookAheadTarget)
                {
                    m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
                }
                else
                {
                    m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
                }

				Vector3 underPos = new Vector3(target2.position.x, target2.position.y - 9f, target2.position.z);
				Vector3 aheadTargetPos = underPos + m_LookAheadPos + Vector3.forward * m_OffsetZ;
                Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

                transform.position = newPos;

                m_LastTargetPosition = target2.position;
            }
        }
    }
}