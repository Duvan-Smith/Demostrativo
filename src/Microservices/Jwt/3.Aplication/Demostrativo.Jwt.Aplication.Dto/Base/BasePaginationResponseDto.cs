namespace Demostrativo.Jwt.Aplication.Dto.Base;

public class BasePaginationResponseDto : DataTransferObjectBase
{
    public int ActualPage { get; set; }
    public int TotalRecords { get; set; }
    public int RecordsPage { get; set; }
}