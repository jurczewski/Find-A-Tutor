use FindATutor

SELECT TOP 1000 *
FROM PrivateLessons(NOLOCK)

SELECT TOP 1000 *
FROM SchoolSubjects(NOLOCK)

SELECT TOP 1000 *
FROM Users(NOLOCK)

select top 1000 u.FirstName, u.LastName, u.[Role], pk.[Description], pk.CreatedAt, pk.RelevantTo, s.[Name] from PrivateLessons pk
join SchoolSubjects s on pk.SchoolSubjectId = s.Id
join Users u on pk.StudentId = u.Id

SELECT TOP 1000 * FROM NLog