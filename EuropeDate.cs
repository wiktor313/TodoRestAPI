using System;

public static class EuropeDate
{
	public static int EuropeDateConverter(this DayOfWeek dayOfWeek)
	{
        return ((int)dayOfWeek + 6) % 7;
    }
}
