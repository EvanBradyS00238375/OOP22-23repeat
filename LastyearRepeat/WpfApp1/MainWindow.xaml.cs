using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Team> teams;

        public MainWindow()
        {
            InitializeComponent();
            GenerateAndDisplayTeams();
        }

        private void GenerateAndDisplayTeams()
        {
            var members = RandomMemberGenerator.GenerateRandomMembers(28);
            teams = CreateTeams(members);
            teamListBox.ItemsSource = teams;
        }

        private List<Team> CreateTeams(List<Member> members)
        {
            var senior = new Team { TeamName = "Senior" };
            var u18 = new Team { TeamName = "U18" };
            var u16 = new Team { TeamName = "U16" };
            var u14 = new Team { TeamName = "U14" };

            foreach (var member in members)
            {
                var age = member.GetRaceAge();

                if (age >= 18)
                {
                    senior.Players.Add(member);
                }
                else if (age >= 16)
                {
                    u18.Players.Add(member);
                }
                else if (age >= 14)
                {
                    u16.Players.Add(member);
                }
                else
                {
                    u14.Players.Add(member);
                }
            }

            return new List<Team> { senior, u18, u16, u14 };
        }

        private void TeamListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTeam = teamListBox.SelectedItem as Team;
            if (selectedTeam != null)
            {
                memberListBox.ItemsSource = selectedTeam.Players;
                selectedMemberTextBlock.Text = $"Selected Team: {selectedTeam.TeamName}";
            }
        }

        private void ShowMemberDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedMember = memberListBox.SelectedItem as Member;
            if (selectedMember != null)
            {
                selectedMemberTextBlock.Text = $"{selectedMember.LastName}, {selectedMember.FirstName} - {selectedMember.Dob.ToShortDateString()} ({selectedMember.GetRaceAge()})";
            }
        }

        private void RandomiseButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateAndDisplayTeams();
        }
    }
}