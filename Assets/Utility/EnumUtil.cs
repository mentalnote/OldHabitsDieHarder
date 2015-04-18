using System.Collections;

public class EnumUtil
{
	//Converts the values of an enum to an int array
	static public int[] ArrayFromEnum(System.Type enumType) {
		return (int[])(System.Enum.GetValues(enumType));
	}

	//Converts a list of enum values to an int array
	static public int[] ValuesToArray<T>(T[] values)
	{
		int[] result = new int[ values.Length ];

		for (int i = 0; i < values.Length; ++i) {
			result[i] = (int)System.Convert.ChangeType(values[i], typeof(int));
		}

		return result;
	}
}
