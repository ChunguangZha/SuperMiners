update superminers.playersimpleinfo s1 set s1.LockedLoginTime = date_sub(now(), interval 1 month)
where s1.LockedLogin = true and s1.LockedLoginTime is null;

-- -------------------------------------


