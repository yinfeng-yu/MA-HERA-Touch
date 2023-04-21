using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : GridLayoutGroup
{
    public int ColumnCount = 2;
    public int RowCount = 4;


    public override void SetLayoutHorizontal()
    {
        UpdateCellSize();
        base.SetLayoutHorizontal();
    }

    public override void SetLayoutVertical()
    {
        UpdateCellSize();
        base.SetLayoutVertical();
    }

    private void UpdateCellSize()
    {
        float x = (rectTransform.rect.size.x - padding.horizontal - spacing.x * (ColumnCount - 1)) / ColumnCount;
        float y = (rectTransform.rect.size.y - padding.vertical - spacing.y * (RowCount - 1)) / RowCount;
        this.constraint = Constraint.FixedColumnCount;
        this.constraintCount = ColumnCount;
        this.cellSize = new Vector2(x, y);
    }
}