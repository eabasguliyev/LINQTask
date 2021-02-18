using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQTask
{
	class Program
	{
		static void Main(string[] args)
		{
			var debtors = DebtorContainer.GetDebtors();

			//1) rhyta.com ve ya dayrep.com domenlerinde emaili olan borclulari cixartmag

			var debtorsByDomains = debtors
				.Where(d =>
				{
					var domainAddress = d.Email.Split('@')[1];

					if (domainAddress == "rhyta.com" || domainAddress == "dayrep.com")
						return true;
					return false;
				})
				.ToList();

			//debtorsByDomains.ForEach(Console.WriteLine);
			
			//2) Yashi 26 - dan 36 - ya qeder olan borclulari cixartmag

			var debtorsByAge = debtors.Where(d =>
			{
				var age = DateTime.Now.Year - d.BirthDay.Year;

				if (age > 25 && age < 36)
					return true;
				return false;
			})
				.ToList();

			//debtorsByAge.ForEach(Console.WriteLine);
			
			//3) Borcu 5000 - den cox olmayan borclularic cixartmag

			var debtorsByDebt = debtors
				.Where(d => d.Debt <= 5000)
				.ToList();

			//debtorsByDebt.ForEach(Console.WriteLine);

			//4) Butov adi 18 simvoldan cox olan ve telefon nomresinde 2 ve ya 2 - den cox 7 reqemi olan borclulari cixartmaq

			var debtorsByNameLength = debtors
				.Where(d => d.FullName.Length > 18)
				.Where(d => d.Phone.Count(c => c == '7') >= 2)
				.ToList();
			

			//debtorsByNameLength.ForEach(Console.WriteLine);


			//5) Qishda anadan olan borclulari cixardmaq

			var onlyWinterDebtors = debtors
				.Where(d => d.BirthDay.Month == 12 || d.BirthDay.Month == 1 || d.BirthDay.Month == 2)
				.ToList();

			//onlyWinterDebtors.ForEach(Console.WriteLine);

			//6) Borcu, umumi borclarin orta borcunnan cox olan borclulari cixarmaq ve evvel familyaya gore sonra ise meblegin azalmagina gore sortirovka etmek

			var averageDebt = debtors.Average(d => d.Debt);

			var debtorsHigherThanAvg = debtors.
				Where(d => d.Debt > averageDebt)
				.ToList();

			var debtorsSortedBySurname = debtorsHigherThanAvg.OrderBy(d =>
			{
				var nameComponents = d.FullName.Split(' ');

				return nameComponents[nameComponents.Length - 1];
			})
				.ToList();
			
			//debtorsSortedBySurname.ForEach(Console.WriteLine);

			var descDebtorsSortedByDebt = debtorsHigherThanAvg
				.OrderByDescending(d => d.Debt)
				.ToList();
		   
			//debtorsSortedBySurname.ForEach(Console.WriteLine);


			//7) Telefon nomresinde 8 olmayan borclularin yalniz familyasin, yashin ve umumi borcun meblegin cixarmaq

			var debtors3 = debtors
				.Where(d => !d.Phone.Contains('8'))
				.ToList();

			debtors3.ForEach(d =>
			{
				var nameComponents = d.FullName.Split(' ');

				//Console.WriteLine($"{nameComponents[nameComponents.Length - 1]} {DateTime.Now.Year - d.BirthDay.Year} {d.Phone} {d.Debt}");
			});


			//8)Adinda ve familyasinda hec olmasa 3 eyni herf olan borclularin siyahisin cixarmaq ve onlari elifba sirasina gore sortirovka elemek

			var debtors4 = debtors
				.Where(d => d.FullName.CheckOccurrence(3))
				.OrderBy(d => d.FullName)
				.ToList();

			//debtors4.ForEach(Console.WriteLine);

			//9)borclulardan en coxu hansi ilde dogulubsa hemin ili cixartmaq

			var years = debtors.Select(d => d.BirthDay.Year).ToList();

			var year = years.GetMostOccurrenceData();

			//Console.WriteLine(year);

			//10)Borcu en boyuk olan 5 borclunun siyahisini cixartmaq

			var firstFiveHigherDebtors = debtors
				.OrderByDescending(d => d.Debt)
				.Take(5)
				.ToList();

			//firstFiveHigherDebtors.ForEach(Console.WriteLine);
			
			//11)Butun borcu olanlarin borcunu cemleyib umumi borcu cixartmaq


			var totalDebt = debtors.Sum(d => d.Debt);

			//Console.WriteLine(totalDebt);

			//12)2ci dunya muharibesin gormush borclularin siyahisi cixartmaq

			var debtorsBySecondWoW = debtors
				.Where(d => d.BirthDay.Year >= 1939 &&
							d.BirthDay.Year <= 1945)
				.ToList();

			//debtorsBySecondWoW.ForEach(Console.WriteLine);
			
			//13)Nomresinde tekrar reqemler olmayan borclularin ve onlarin borcunun meblegin cixartmaq

            var debtors5 = debtors.Where(d =>
            {
                for (var i = 0; i < d.Phone.Length; i++)
                {
                    if (!char.IsDigit(d.Phone[i]))
                        continue;

                    if (d.Phone.Count(n => n == d.Phone[i]) != 1)
                        return false;
				}

                return true;
            }).ToList();

			debtors5.ForEach(d =>
            {
                //Console.WriteLine($"{d.FullName} {d.Debt}");
            });

			//14)Tesevvur edek ki,butun borclari olanlar bugunden etibaren her ay 500 azn pul odeyecekler.Oz ad gunune kimi borcun oduyub qurtara bilenlerin siyahisin cixartmaq

            var debtors6 = debtors.Where(d =>
            {
                var birthMonth = d.BirthDay.Month;
                var debt = d.Debt;

                for (int i = DateTime.Now.Month; i <= birthMonth; i++)
                {
                    if (debt % 500 >= 0)
                        debt -= 500;
                    else
                        debt = 0;
                }

                if (debt == 0)
                    return true;

                return false;
            }).ToList();

			//debtors6.ForEach(Console.WriteLine);
			
            //15)Adindaki ve familyasindaki herflerden "smile" sozunu yaza bileceyimiz borclularin siyahisini cixartmaq

            var debtors7 = debtors.Where(d =>
            {
                var fullname = d.FullName.ToLower();
                foreach (var character in "smile")
                {
                    if (!fullname.Contains(character))
                        return false;
                }

                return true;
            }).ToList();

			//debtors7.ForEach(Console.WriteLine);

			Console.ReadLine();
		}

	}
}