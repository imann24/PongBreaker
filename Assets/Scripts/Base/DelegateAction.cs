/*
 * Author: Isaiah Mann
 * Description: Subscribable action structure
 */

using System;
using System.Collections.Generic;

public class DelegateAction
{
	Action onCall;
	object owner;
	HashSet<object> approvedCallers;

	public DelegateAction(object owner)
	{
		this.owner = owner;
	}

	public void Call(object caller)
	{
		if(onCall != null)
		{
			onCall();
		}
	}

	public void Subscribe(Action action)
	{
		onCall += action;
	}

	public void Unsubscribe(Action action)
	{
		onCall -= action;
	}

	public void AddApprovedCaller(object owner, object caller)
	{
		if(isOwner(owner))
		{
			approvedCallers.Add(caller);
		}
	}

	public void RemoveApprovedCaller(object owner, object caller)
	{
		if(isOwner(owner))
		{
			approvedCallers.Remove(caller);
		}
	}

	bool validateCall(object caller)
	{
		return isOwner(caller) || approvedCallers.Contains(caller);
	}

	bool isOwner(object compare)
	{
		return compare == owner;
	}
}
