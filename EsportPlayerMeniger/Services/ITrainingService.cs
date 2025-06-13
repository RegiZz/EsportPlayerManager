using System.Collections.Generic;
using EsportPlayerMeniger.Models;
using System.Threading.Tasks;

namespace EsportPlayerMeniger.Services;

public interface ITrainingService
{
    Task<List<Training>> GetAllTrainingsAsync();
    Task<Training?> GetTrainingByIdAsync(int id);
    Task AddTrainingAsync(Training training);
    Task DeleteTrainingAsync(Training training);
    Task<bool> CanPlayerTrain(Player player);
    Task<TrainingResult> TrainPlayer(Player player, Training training);
}