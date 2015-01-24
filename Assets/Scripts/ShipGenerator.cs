using System;
using System.Security.Principal;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class ShipGenerator : MonoBehaviour
{
	public GameObject[] hullPrefabs;
	public GameObject[] itemPrefabs;

	enum Side { Left, Right, Top, Bottom };
	public class Slot
	{
		public Slot(bool item, Vector2 position)
		{
			this.item = item;
			this.position = position;
		}

		public GameObject gameObject;
		public bool used;
		public bool item;
		public Vector2 position;

		public Slot left;
		public Slot right;
		public Slot top;
		public Slot bottom;
	}
	public List<Slot> slots = new List<Slot>();

	Slot[,] slotMap = new Slot[100,100];

	public int itemCount = 10;
	int usedSlotCount;

	void Start()
	{
		GenerateBaseHullSlots();
		 
		GenerateSlots ();

		GenerateParts();

		ConnectParts();
	}

	void ConnectParts()
	{
		foreach (var slot in slots)
		{
			if (slot.gameObject == null)
				continue;

			SpringJoint2D joint;
			// because it said it was not assigned :(
			joint = null;

			slot.left =		 GetSlotFromMap (slot.position + new Vector2 (-1, 0));
			slot.right =	 GetSlotFromMap (slot.position + new Vector2 (1, 0));
			slot.top =		 GetSlotFromMap (slot.position + new Vector2 (0, 1));
			slot.bottom =	 GetSlotFromMap (slot.position + new Vector2 (0, -1));

			AddJoint (slot, slot.left, new Vector2(-0.5f, 0));
			AddJoint (slot, slot.right, new Vector2 (0.5f, 0));
			AddJoint (slot, slot.top, new Vector2 (0, 0.5f));
			AddJoint (slot, slot.bottom, new Vector2 (0, -0.5f));
		}
	}

	Slot GetSlotFromMap (Vector2 position)
	{
		int x = (int)position.x + 50;
		int y = (int)position.y + 50;

		return slotMap[x, y];
	}

	void SetSlotToMap(Slot slot, Vector2 position)
	{
		int x = (int)position.x + 50;
		int y = (int)position.y + 50;

		slotMap[x, y] = slot;
	}

	void AddJoint(Slot from, Slot to, Vector2 anchor)
	{
		if (from == null || to == null)
			return;

		var joint = from.gameObject.AddComponent<SpringJoint2D> ();
		joint.distance = 0;
		joint.frequency = from.item ? 10f : 1.5f;
		joint.collideConnected = true;
		joint.anchor = anchor;
		joint.connectedAnchor = -anchor;
		joint.connectedBody = to.gameObject.rigidbody2D;
	}

	void GenerateParts()
	{
		foreach (var slot in slots)
		{
			var prefab = slot.item
				? itemPrefabs[Random.Range(0, itemPrefabs.Length)]
				: hullPrefabs[Random.Range(0, hullPrefabs.Length)];

			slot.gameObject = Instantiate (prefab, slot.position, prefab.transform.rotation) as GameObject;
			slot.gameObject.transform.parent = transform;
            
		}
	}

	void GenerateBaseHullSlots()
	{
		var slot = new Slot (false, Vector2.zero);
		slots.Add (slot);
		SetSlotToMap (slot, slot.position);
		slot.used = true;

		AddSlot (slot, Side.Left, false);
		AddSlot (slot, Side.Right, false);
		AddSlot (slot, Side.Top, false);
		AddSlot (slot, Side.Bottom, false);
	}

	void GenerateSlots ()
	{
		var shuffledSlots = slots.ToArray();
		Shuffle (shuffledSlots);

		bool done = true;

		foreach (var slot in shuffledSlots)
		{
			if (slot.used || slot.item)
				continue;
			done = false;

			slot.used = true;
			int[] array = {1, 2, 3, 4};
			Shuffle(array);

			AddSlot (slot, Side.Left,	array[0] > 3);
			AddSlot (slot, Side.Right,	array[1] > 3);
			AddSlot (slot, Side.Top,	array[2] > 3);
			AddSlot (slot, Side.Bottom, array[3] > 3);
		}

		if (!done && usedSlotCount < itemCount)
			GenerateSlots ();
	}

	Slot AddSlot(Slot slot, Side side, bool item)
	{
		if (item && usedSlotCount >= itemCount)
			return null;

		Vector2 direction =
			side == Side.Left ? new Vector2 (-1, 0) :
			side == Side.Right ? new Vector2 (1, 0) :
			side == Side.Top ? new Vector2 (0, 1) :
			side == Side.Bottom ? new Vector2 (0, -1) : Vector2.zero;

		Vector2 position = slot.position + direction;

		if (GetSlotFromMap(position) != null)
			return null;

		Slot target =
			side == Side.Left ? slot.left :
			side == Side.Right ? slot.right :
			side == Side.Top ? slot.top :
			side == Side.Bottom ? slot.bottom : null;

		target = new Slot (item, position);

		usedSlotCount += item ? 1 : 0;
		slots.Add (target);

		SetSlotToMap (target, position);

		return target;
	}

	public static void Shuffle<T>(T[] array)
	{
		int n = array.Length;
		for (int i = 0; i < n; i++)
		{
			int r = i + (int)(Random.value * (n - i));
			T t = array[r];
			array[r] = array[i];
			array[i] = t;
		}
	}
}
