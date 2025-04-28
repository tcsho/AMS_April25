-- GetEmployeeLeaves4Adjustment 0,0,'10117',0   
-- GetEmployeeLeaves4Adjustment 0,0,'',0   
-- Create By: Rumman  
-- Created Date: 17/04/2025  
-- Modified By: Rumman  
-- Last Modified Date: 24/04/2025  
alter PROCEDURE GetEmployeeLeaves4Adjustment  
 @RegionId INT = 0,      
 @CenterId INT = 0,  
 @EmployeeCode varchar(50),  
 @LeaveType_Id INT = 0  
AS          
BEGIN    



DECLARE @PmonthPrevious NVARCHAR(50) = '202504'
  
--DECLARE @PmonthPrevious NVARCHAR(50) = (       
--SELECT PMonth      
--FROM Period      
--WHERE CONVERT(DATE, PDate) = CONVERT(DATE, DATEADD(DAY, -1, GETDATE()))                          
--         )  
  
--DELETE FROM LeavesUploadERP WHERE FinalID in ( SELECT l1.FinalID FROM LeavesUploadERP l1 JOIN LeavesUploadERP l2 ON l1.EmployeeCode = l2.EmployeeCode WHERE l1.PMonth = @PmonthPrevious   AND l1.LeaveType_Id = 6070   AND l2.PMonth = @PmonthPrevious   AND l2
--.LeaveType_Id = 6071   AND l1.LeaveFrom <= l2.LeaveTo   AND l1.LeaveTo >= l2.LeaveFrom   );  
  
--  SELECT   
--    lue.FinalID,  
--    lue.EmployeeCode,   
--    u.FullName,      
--    d.DesigName,      
--    elt.LeaveType,  
--    lue.LeaveDays,  
--    ISNULL(lue.EmpReason, '-') AS EmpReason,  
--    lue.LeaveFrom,  
--    lue.leaveto,   
--    ISNULL(EmployeeCurrentLeaveBalanceActual.TCasualLeave, 0) AS balCasual,  
--    ISNULL(EmployeeCurrentLeaveBalanceActual.AnnulaLeave, 0) AS balAnnual,  
--    HR_LeaveType_Id,  
--    HR_LeaveFrom,  
--    HR_LeaveTo,  
--    lue.HRComment   
--FROM LeavesUploadERP AS lue  
--INNER JOIN (  
--    SELECT EmployeeCodeI, MAX(CreatedOn) AS MaxCreatedOn  
--    FROM EmployeeCurrentLeaveBalanceActual_Log  
--    GROUP BY EmployeeCodeI  
--) AS LatestBalance  
--    ON lue.EmployeeCode = LatestBalance.EmployeeCodeI  
--INNER JOIN EmployeeCurrentLeaveBalanceActual_Log AS EmployeeCurrentLeaveBalanceActual   
--    ON EmployeeCurrentLeaveBalanceActual.EmployeeCodeI = LatestBalance.EmployeeCodeI  
--    AND EmployeeCurrentLeaveBalanceActual.CreatedOn = LatestBalance.MaxCreatedOn  
--LEFT JOIN EmployeeProfile AS u ON lue.EmployeeCode = u.EmployeeCode        
--JOIN designation d ON u.DesigCode = d.DesigCode      
--LEFT JOIN EmployeeLeaveType elt ON lue.LeaveType_Id = elt.LeaveType_Id   
--WHERE       
--    ISNULL(lue.Region_Id, 0) = @RegionId       
--    AND ISNULL(lue.Center_Id, 0) = @CenterId  
--    AND (@EmployeeCode IS NULL OR lue.EmployeeCode = @EmployeeCode)  
--    AND PMonth = @PmonthPrevious      
--    AND (@LeaveType_Id = 0 OR lue.LeaveType_Id = @LeaveType_Id)    
--ORDER BY lue.EmployeeCode;  
  
  
--  WITH LatestLeaveBalance AS (  
--    SELECT *,  
--           ROW_NUMBER() OVER (  
--               PARTITION BY EmployeeCodeI  
--               ORDER BY CreatedOn DESC  
--           ) AS rn  
--    FROM EmployeeCurrentLeaveBalanceActual_Log  
--)  
  
--SELECT   
--    lue.FinalID,  
--    lue.EmployeeCode,   
--    u.FullName,      
--    d.DesigName,      
--    elt.LeaveType,  
--    lue.LeaveDays,  
--    ISNULL(lue.EmpReason,'-') AS EmpReason,  
--    lue.LeaveFrom,  
--    lue.leaveto,   
--    ISNULL(EmployeeCurrentLeaveBalanceActual.TCasualLeave, 0) AS balCasual,  
--    ISNULL(EmployeeCurrentLeaveBalanceActual.AnnulaLeave, 0) AS balAnnual,  
--    HR_LeaveType_Id,  
--    HR_LeaveFrom,  
--    HR_LeaveTo,  
--    lue.HRComment   
--FROM LeavesUploadERP AS lue  
--INNER JOIN LatestLeaveBalance AS EmployeeCurrentLeaveBalanceActual   
--    ON lue.EmployeeCode = EmployeeCurrentLeaveBalanceActual.EmployeeCodeI  
--    AND EmployeeCurrentLeaveBalanceActual.rn = 1  
--LEFT JOIN EmployeeProfile AS u ON lue.EmployeeCode = u.EmployeeCode        
--JOIN designation d ON u.DesigCode = d.DesigCode      
--LEFT JOIN EmployeeLeaveType elt ON lue.LeaveType_Id = elt.LeaveType_Id   
--WHERE       
--    ISNULL(lue.Region_Id, 0) = @RegionId       
--    AND ISNULL(lue.Center_Id, 0) = @CenterId  
--    AND (@EmployeeCode IS NULL OR lue.EmployeeCode = @EmployeeCode)  
--    AND PMonth = @PmonthPrevious      
--    AND (@LeaveType_Id = 0 OR lue.LeaveType_Id = @LeaveType_Id)    
--ORDER BY lue.EmployeeCode;  
   
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
 ISNULL(EmployeeCurrentLeaveBalanceActual.TAnnulaLeave,0) AS balAnnual,  
 HR_LeaveType_Id,  
 HR_LeaveFrom,  
 HR_LeaveTo,  
 lue.HRComment   
        FROM LeavesUploadERP AS lue  
  --INNER JOIN [ORACLE]..[AIMS].[VW_TCS_ERP_CURLEAVEBAL] AS EmployeeCurrentLeaveBalanceActual ON  lue.EmployeeCode = EmployeeCurrentLeaveBalanceActual.EmployeeCodeI   
  INNER JOIN EmpLeaveBalance4LeaveAdjustment AS EmployeeCurrentLeaveBalanceActual ON  lue.EmployeeCode = EmployeeCurrentLeaveBalanceActual.EmployeeCode  
  LEFT JOIN EmployeeProfile AS u ON lue.EmployeeCode = u.EmployeeCode        
  JOIN designation d ON u.DesigCode = d.DesigCode      
  LEFT JOIN EmployeeLeaveType elt ON lue.LeaveType_Id = elt.LeaveType_Id   
        WHERE       
  ISNULL(lue.Region_Id,0) = @RegionId       
  AND ISNULL(lue.Center_Id,0) = @CenterId  
  AND (@EmployeeCode IS NULL OR lue.EmployeeCode = @EmployeeCode)  
  AND lue.PMonth = @PmonthPrevious     
  AND EmployeeCurrentLeaveBalanceActual.PMonth = @PmonthPrevious  
  AND (@LeaveType_Id = 0 OR lue.LeaveType_Id = @LeaveType_Id)    
  ORDER BY lue.EmployeeCode  
END; 