using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;

    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }

            return instance;
        }
    }
    
    [Space(10)]
    [Header("地牢")]
    public RoomNodeTypeListSO roomNodeTypeList;
    public CurrentPlayerSO currentPlayer;
    //昏暗材质
    public Material dimmedMaterial;
    public Material litMaterial;
    public Material variableLitShader;

	#region Validation

#if UNITY_EDITOR

	private void OnValidate()
	{
        HelperUtilities.ValidateCheckNullValue(this, nameof(roomNodeTypeList), roomNodeTypeList);
		HelperUtilities.ValidateCheckNullValue(this, nameof(currentPlayer), currentPlayer);
		HelperUtilities.ValidateCheckNullValue(this, nameof(litMaterial), litMaterial);
		HelperUtilities.ValidateCheckNullValue(this, nameof(dimmedMaterial), dimmedMaterial);
		HelperUtilities.ValidateCheckNullValue(this, nameof(variableLitShader), variableLitShader);
	}

#endif

	#endregion
}
