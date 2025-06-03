using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace KelimeEzberApp
{
    public partial class FormImageUpdater : Form
    {
        public FormImageUpdater()
        {
            InitializeComponent();
        }

        private void FormImageUpdater_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> wordImagePaths = new Dictionary<string, string>()
            {
                { "Apple", "images/apple.jpg" },
                { "Book", "images/book.jpg" },
                { "Car", "images/car.jpg" },
                { "Dog", "images/dog.jpg" },
                { "Egg", "images/egg.jpg" },
                { "Fish", "images/fish.jpg" },
                { "Game", "images/game.jpg" },
                { "House", "images/house.jpg" },
                { "Ice", "images/ice.jpg" },
                { "Juice", "images/juice.jpg" },
                { "Kite", "images/kite.jpg" },
                { "Lion", "images/lion.jpg" },
                { "Moon", "images/moon.jpg" },
                { "Nose", "images/nose.jpg" },
                { "Orange", "images/orange.jpg" },
                { "Pen", "images/pen.jpg" },
                { "Queen", "images/queen.jpg" },
                { "Rain", "images/rain.jpg" },
                { "Sun", "images/sun.jpg" },
                { "Tree", "images/tree.jpg" },
                { "Umbrella", "images/umbrella.jpg" },
                { "Van", "images/van.jpg" },
                { "Window", "images/window.jpg" },
                { "Xylophone", "images/xylophone.jpg" },
                { "Yogurt", "images/yogurt.jpg" },
                { "Zebra", "images/zebra.jpg" },
                { "Bag", "images/bag.jpg" },
                { "Clock", "images/clock.jpg" },
                { "Door", "images/door.jpg" },
                { "Ear", "images/ear.jpg" },
                { "Fan", "images/fan.jpg" },
                { "Glass", "images/glass.jpg" },
                { "Hat", "images/hat.jpg" },
                { "Island", "images/island.jpg" },
                { "Jacket", "images/jacket.jpg" },
                { "Key", "images/key.jpg" },
                { "Lamp", "images/lamp.jpg" },
                { "Mirror", "images/mirror.jpg" },
                { "Notebook", "images/notebook.jpg" },
                { "Oven", "images/oven.jpg" },
                { "Pencil", "images/pencil.jpg" },
                { "Quilt", "images/quilt.jpg" },
                { "Radio", "images/radio.jpg" },
                { "Shoe", "images/shoe.jpg" },
                { "Table", "images/table.jpg" },
                { "Uniform", "images/uniform.jpg" },
                { "Vase", "images/vase.jpg" },
                { "Watch", "images/watch.jpg" },
                { "X-ray", "images/x-ray.jpg" },
                { "Yard", "images/yard.jpg" },
                { "Zoo", "images/zoo.jpg" },
                { "Air", "images/air.jpg" },
                { "Ball", "images/ball.jpg" },
                { "Cat", "images/cat.jpg" },
                { "Drum", "images/drum.jpg" },
                { "Engine", "images/engine.jpg" },
                { "Flag", "images/flag.jpg" },
                { "Goat", "images/goat.jpg" },
                { "Hill", "images/hill.jpg" },
                { "Ink", "images/ink.jpg" },
                { "Jungle", "images/jungle.jpg" },
                { "Kangaroo", "images/kangaroo.jpg" },
                { "Leaf", "images/leaf.jpg" },
                { "Mountain", "images/mountain.jpg" },
                { "Net", "images/net.jpg" },
                { "Octopus", "images/octopus.jpg" },
                { "Pizza", "images/pizza.jpg" },
                { "Ring", "images/ring.jpg" },
                { "Star", "images/star.jpg" },
                { "Truck", "images/truck.jpg" },
                { "Unicorn", "images/unicorn.jpg" },
                { "Violin", "images/violin.jpg" },
                { "Whistle", "images/whistle.jpg" },
                { "Xenon", "images/xenon.jpg" },
                { "Yarn", "images/yarn.jpg" },
                { "Zipper", "images/zipper.jpg" },
                { "Ant", "images/ant.jpg" },
                { "Beach", "images/beach.jpg" },
                { "Candle", "images/candle.jpg" },
                { "Doll", "images/doll.jpg" },
                { "Elephant", "images/elephant.jpg" },
                { "Fork", "images/fork.jpg" },
                { "Guitar", "images/guitar.jpg" },
                { "Hammer", "images/hammer.jpg" },
                { "Iron", "images/iron.jpg" },
                { "Jelly", "images/jelly.jpg" },
                { "Ladder", "images/ladder.jpg" },
                { "Mouse", "images/mouse.jpg" },
                { "Nest", "images/nest.jpg" },
                { "Owl", "images/owl.jpg" },
                { "Parrot", "images/parrot.jpg" },
                { "Quokka", "images/quokka.jpg" },
                { "Rose", "images/rose.jpg" },
                { "Snake", "images/snake.jpg" },
                { "Tiger", "images/tiger.jpg" },
                { "Vulture", "images/vulture.jpg" },
                { "Whale", "images/whale.jpg" },
            };

            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                foreach (var entry in wordImagePaths)
                {
                    string update = "UPDATE Words SET ImagePath = @img WHERE English = @eng";
                    using (SQLiteCommand cmd = new SQLiteCommand(update, conn))
                    {
                        cmd.Parameters.AddWithValue("@img", entry.Value);
                        cmd.Parameters.AddWithValue("@eng", entry.Key);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Tüm görseller başarıyla güncellendi!");
        }
    }
}
