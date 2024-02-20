using System.Collections.Generic;
using UnityEngine;

internal static class YieldInstructionCache // ������ ������̶� MonoBehaviour�� �ʿ����. ��� internal�� �̱��� ����� ȿ���� ��.
{
    //public static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame(); // ���� ������ ������ �� �ִ� �ǹ̷� �ۼ��� �ڵ�. �� ������Ʈ������ ���� ����.

    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();
    //                          �� cpp�� Map<key, value> �� ����������.
    public static WaitForSeconds WaitForSeconds(float second)
    {
        WaitForSeconds wfs;

        if (!waitForSeconds.TryGetValue(second, out wfs)) // Dictionary�� ��ġ�ϴ� Ű ���� ���� ���� ���(������ (out wfs)���� Dictionary ���� ���� ������.)
        {
            waitForSeconds.Add(second, wfs = new WaitForSeconds(second)); // �Էµ� Ű ������ �� ��ü�� ����� Dictionary�� ������.
        }
        return wfs;
    }

    private static readonly Dictionary<float, WaitForSecondsRealtime> waitForSecondsRealtime = new Dictionary<float, WaitForSecondsRealtime>();
    //                          �� cpp�� Map<key, value> �� ����������.
    public static WaitForSecondsRealtime WaitForSecondsRealtime(float second)
    {
        WaitForSecondsRealtime wfs;

        if (!waitForSecondsRealtime.TryGetValue(second, out wfs)) // Dictionary�� ��ġ�ϴ� Ű ���� ���� ���� ���(������ (out wfs)���� Dictionary ���� ���� ������.)
        {
            waitForSecondsRealtime.Add(second, wfs = new WaitForSecondsRealtime(second)); // �Էµ� Ű ������ �� ��ü�� ����� Dictionary�� ������.
        }
        return wfs;
    }

    private static WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    public static WaitForFixedUpdate WaitFor_FixedUpdate => waitForFixedUpdate;
}