using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private Line _linePrefab;

    private List<Line> _lines = new List<Line>();

    public void CustomUpdate()
    {
        foreach (var line in _lines)
            line.CustomUpdate();
    }

    public Line GetLine(Transform startPoint)
    {
        Line newLine = Instantiate(_linePrefab, transform);
        _lines.Add(newLine);

        newLine.Init(startPoint);
        return newLine;
    }

    public void RemoveLine(Line line)
    {
        _lines.Remove(line);

        Destroy(line.gameObject);
    }
}
