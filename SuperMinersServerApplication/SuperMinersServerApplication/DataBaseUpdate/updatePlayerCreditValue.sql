drop table  if exists tmp_table;
CREATE TEMPORARY TABLE tmp_table  select b.*, s.ValueRMB from superminers.buystonesrecord b 
left join superminers.sellstonesorder s on b.OrderNumber = s.OrderNumber ;

drop table  if exists tmp_table2;
CREATE TEMPORARY TABLE tmp_table2 select BuyerUserName, sum(ValueRMB) as ValueRMB 
from tmp_table
group by BuyerUserName;


drop table  if exists tmp_table3;
CREATE TEMPORARY TABLE tmp_table3 select t2.*, s.id as UserID  
from tmp_table2 t2 left join superminers.playersimpleinfo s on s.UserName = t2.BuyerUserName;


update  superminers.playerfortuneinfo f set f.CreditValue = 
(
	select t.ValueRMB from tmp_table2 t where t.BuyerUserName =(
		select u.UserName from superminers.playersimpleinfo u 
        where u.id = f.userId
    )
)
where f.userId in ( select UserID from tmp_table3 )
;

drop table  if exists tmp_table;
drop table  if exists tmp_table2;
drop table  if exists tmp_table3;
