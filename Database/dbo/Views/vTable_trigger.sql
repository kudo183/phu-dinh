

CREATE VIEW [dbo].[vTable_trigger]
AS
SELECT  
       TAB.name as Table_Name 
     , TRIG.name as Trigger_Name
     , TRIG.is_disabled  --or objectproperty(object_id('TriggerName'), 'ExecIsTriggerDisabled')
FROM [sys].[triggers] as TRIG 
inner join sys.tables as TAB 
on TRIG.parent_id = TAB.object_id