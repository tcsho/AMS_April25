-- GetEmployeeLeaves4Adjustment 0,0,'10117',0 
alter PROCEDURE GetEmployeeLeaves4Adjustment
 @RegionId INT = 0,    
 @CenterId INT = 0,
 @EmployeeCode varchar(50),
 @LeaveType_Id INT = 0
AS        
BEGIN  

DECLARE @PmonthPrevious NVARCHAR(50) = (     
SELECT PMonth    
FROM Period    
WHERE CONVERT(DATE, PDate) = CONVERT(DATE, DATEADD(DAY, -1, GETDATE()))                        
         )

DELETE FROM LeavesUploadERP WHERE FinalID in (
SELECT l1.FinalID
FROM LeavesUploadERP l1
JOIN LeavesUploadERP l2 ON l1.EmployeeCode = l2.EmployeeCode
WHERE l1.PMonth = @PmonthPrevious
  AND l1.LeaveType_Id = 6070
  AND l2.PMonth = @PmonthPrevious
  AND l2.LeaveType_Id = 6071
  AND l1.LeaveFrom <= l2.LeaveTo
  AND l1.LeaveTo >= l2.LeaveFrom
  );

 SELECT lue.FinalID,
 lue.EmployeeCode, 
 u.FullName,    
 d.DesigName,    
 elt.LeaveType,
 lue.LeaveDays,
 ISNULL(lue.EmpReason,'-') AS EmpReason,
 lue.LeaveFrom,
 lue.leaveto, 
ISNULL(EmployeeCurrentLeaveBalanceActual.TCasualLeave,0) AS balCasual,
 ISNULL(EmployeeCurrentLeaveBalanceActual.AnnulaLeave,0) AS balAnnual,
 HR_LeaveType_Id,
 HR_LeaveFrom,
 HR_LeaveTo,
 lue.HRComment 
        FROM LeavesUploadERP AS lue
		INNER JOIN [ORACLE]..[AIMS].[VW_TCS_ERP_CURLEAVEBAL] AS EmployeeCurrentLeaveBalanceActual ON  lue.EmployeeCode = EmployeeCurrentLeaveBalanceActual.EmployeeCodeI 
  LEFT JOIN EmployeeProfile AS u ON lue.EmployeeCode = u.EmployeeCode      
  JOIN designation d ON u.DesigCode = d.DesigCode    
  LEFT JOIN EmployeeLeaveType elt ON lue.LeaveType_Id = elt.LeaveType_Id

        WHERE     
  ISNULL(lue.Region_Id,0) = @RegionId     
  AND ISNULL(lue.Center_Id,0) = @CenterId
  AND (@EmployeeCode IS NULL OR lue.EmployeeCode = @EmployeeCode)
  AND PMonth = @PmonthPrevious    
  AND (@LeaveType_Id = 0 OR lue.LeaveType_Id = @LeaveType_Id)  
  ORDER BY lue.EmployeeCode
END;     