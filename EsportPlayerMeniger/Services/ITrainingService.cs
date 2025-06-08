using System.Collections.Generic;
using System.Threading.Tasks;
using EsportPlayerMeniger.Models;

namespace EsportPlayerMeniger.Services;

public interface ITrainingService
{
    Task<List<Training>> GetAllTrainingsAsync();
    Task<Training?> GetTrainingByIdAsync(int id);
    Task AddTrainingAsync(Training training);
    Task<bool> CanPlayerTrain(Player player);
    Task TrainPlayer(Player player, Training training);
}