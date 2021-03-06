﻿using MagicMirror.DataAccess.Compliments;
using System;

namespace MagicMirror.Business.Services
{
    public class ComplimentService : IComplimentService
    {
        public string GenerateCompliment()
        {
            var compRepo = new ComplimentsRepo().GetCompliments();
            int randomCompliment = new Random().Next(compRepo.Count);
            string compliment = compRepo[randomCompliment];

            return compliment;
        }
    }
}