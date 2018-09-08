using UnityEngine;

public class PaddleController : PhysicalObjectController 
{
	float objectDetachSpeed = 30.0f;

	public PuckController ClosestPuck
	{
		get
		{
			return getClosestPuck();
		}
	}

	protected PuckController puck;

	protected float speed = Global.BASE_PADDLE_SPEED;

	protected GameObject myGoal;

	public PaddlePosition PaddlePosition
	{
		get;
		private set;
	}

    protected bool hasPuckAttached = false;

	Vector3 startingPosition;
	protected GameplayController gameplay;
    Attacher attacher;

	protected override void Awake()
	{
		base.Awake();
		startingPosition = transform.position;
        attacher = GetComponentInChildren<Attacher>();
	}

	protected override void Start()
	{
		base.Start();
		StateController.Instance.CurrentGame.OnRestart.Subscribe(resetPosition);
		gameplay = GameplayController.Instance;
		gameplay.RegisterPlayer(this);

	}

    protected override void OnDestroy()
    {
        base.OnDestroy();
        gameplay.UnregisterPlayer(this);
    }

	protected virtual void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			detachAll();
		}
	}

	public void Initialize(PuckController puck, PaddlePosition paddlePosition, GameObject myGoal)
	{
		this.puck = puck;
		this.PaddlePosition = paddlePosition;
		this.myGoal = myGoal;
	}

	public override string ToString ()
	{
		return string.Format("[PaddleController: {0}]", PaddlePosition);
	}

	public override void Attach(PhysicalObjectController objectToAttach)
	{
		base.Attach(objectToAttach);
		switch(PaddlePosition)
		{
			case PaddlePosition.Left:
				objectToAttach.transform.position += Vector3.right;
				break;
			case PaddlePosition.Right:
				objectToAttach.transform.position += Vector3.left;
				break;
		}
        if(objectToAttach is PuckController)
        {
            hasPuckAttached = true;
        }
	}

	protected override Transform getTransformToAttach()
	{
        return transform;
	}

	public void SetObjectDetachSpeed(float objectDetachSpeed)
	{
		this.objectDetachSpeed = objectDetachSpeed;
	}

	public override void Detach(PhysicalObjectController objectToDetach)
	{
		base.Detach(objectToDetach);
		Rigidbody2D detachedObjectRigibody = objectToDetach.GetComponent<Rigidbody2D>();
		detachedObjectRigibody.rotation = 0;
		if(objectToDetach is PuckController)
		{
			(objectToDetach as PuckController).StartMotion();
            hasPuckAttached = false;
		}
		if(!detachedObjectRigibody)
		{
			return;
		}
		switch(PaddlePosition)
		{
			case PaddlePosition.Left:
                detachedObjectRigibody.velocity += Vector2.right * objectDetachSpeed;
				break;
			case PaddlePosition.Right:
                detachedObjectRigibody.velocity += Vector2.left * objectDetachSpeed;
				break;
		}
	}

	void resetPosition()
	{
		transform.position = startingPosition;
	}

	protected PuckController getClosestPuck()
	{
		PhysicalObjectController closestPuck;
		if(tryGetClosestInstance(gameplay.GetLivePucks().ConvertAll(puck => puck as PhysicalObjectController), out closestPuck))
		{
			if(closestPuck is PuckController)
			{
				return closestPuck as PuckController;
			}
			else
			{
				throw new System.Exception(string.Format("Expected to find object of type {0} but instead found object of type {1}", typeof(PuckController), closestPuck.GetType())); 
			}
		}
		else
		{
			throw new System.Exception("Unable to find any pucks");
		}
	}
}
