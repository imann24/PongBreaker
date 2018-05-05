using UnityEngine;

public class PaddleController : PhysicalObjectController 
{
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

	Vector3 startingPosition;
	GameplayController gameplay;

	protected override void Awake()
	{
		base.Awake();
		startingPosition = transform.position;
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
