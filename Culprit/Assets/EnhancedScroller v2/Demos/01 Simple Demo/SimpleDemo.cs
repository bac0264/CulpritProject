﻿using UnityEngine;
using System.Collections;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System.Collections.Generic;

namespace EnhancedScrollerDemos.SuperSimpleDemo
{
    /// <summary>
    /// Set up our demo script as a delegate for the scroller by inheriting from the IEnhancedScrollerDelegate interface
    /// 
    /// EnhancedScroller delegates will handle telling the scroller:
    ///  - How many cells it should allocate room for (GetNumberOfCells)
    ///  - What each cell size is (GetCellSize)
    ///  - What the cell at a given index should be (GetCell)
    /// </summary>
    public class SimpleDemo : MonoBehaviour, IEnhancedScrollerDelegate
    {
        /// <summary>
        /// Internal representation of our data. Note that the scroller will never see
        /// this, so it separates the data from the layout using MVC principles.
        /// </summary>
        /// 
        public SmallList<Data> _data;
        public SmallList<List<Data>> _dataList;
        /// <summary>
        /// This is our scroller we will be a delegate for
        /// </summary>
        public EnhancedScroller scroller;

        /// <summary>
        /// This will be the prefab of each cell in our scroller. Note that you can use more
        /// than one kind of cell, but this example just has the one type.
        /// </summary>
        public EnhancedScrollerCellView cellViewPrefab;

        /// <summary>
        /// Be sure to set up your references to the scroller after the Awake function. The 
        /// scroller does some internal configuration in its own Awake function. If you need to
        /// do this in the Awake function, you can set up the script order through the Unity editor.
        /// In this case, be sure to set the EnhancedScroller's script before your delegate.
        /// 
        /// In this example, we are calling our initializations in the delegate's Start function,
        /// but it could have been done later, perhaps in the Update function.
        /// </summary>
        public virtual void Start()
        {
            // tell the scroller that this script will be its delegate
            scroller.Delegate = this;
            LoadLargeData();
        }
        /// <summary>
        /// Populates the data with a lot of records
        /// </summary>
        public virtual void LoadLargeData()
        {
            //   amount = 100;
            // set up some simple data
            _data = new SmallList<Data>();
            for (var i = 0; i < 1000; i++)
            {
                _data.Add(new Data()
                {
                    indexStage = i
                });
            }
            // tell the scroller to reload now that we have the data
            scroller.ReloadData();
        }

        /// <summary>
        /// Populates the data with a small set of records
        /// </summary>

        #region UI Handlers

        /// <summary>
        /// Button handler for the large data loader
        /// </summary>
        public void LoadLargeDataButton_OnClick()
        {
            LoadLargeData();
        }

        /// <summary>
        /// Button handler for the small data loader
        /// </summary>
        //public void LoadSmallDataButton_OnClick()
        //{
        //    LoadSmallData();
        //}

        #endregion

        #region EnhancedScroller Handlers

        /// <summary>
        /// This tells the scroller the number of cells that should have room allocated. This should be the length of your data array.
        /// </summary>
        /// <param name="scroller">The scroller that is requesting the data size</param>
        /// <returns>The number of cells</returns>
        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            // in this example, we just pass the number of our data elements
            if (_dataList != null) return _dataList.Count;
            return _data.Count;
        }

        /// <summary>
        /// This tells the scroller what the size of a given cell will be. Cells can be any size and do not have
        /// to be uniform. For vertical scrollers the cell size will be the height. For horizontal scrollers the
        /// cell size will be the width.
        /// </summary>
        /// <param name="scroller">The scroller requesting the cell size</param>
        /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
        /// <returns>The size of the cell</returns>
        public virtual float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            // in this example, even numbered cells are 30 pixels tall, odd numbered cells are 100 pixels tall
            return (dataIndex % 2 == 0 ? 1000 : 1000);
        }

        /// <summary>
        /// Gets the cell to be displayed. You can have numerous cell types, allowing variety in your list.
        /// Some examples of this would be headers, footers, and other grouping cells.
        /// </summary>
        /// <param name="scroller">The scroller requesting the cell</param>
        /// <param name="dataIndex">The index of the data that the scroller is requesting</param>
        /// <param name="cellIndex">The index of the list. This will likely be different from the dataIndex if the scroller is looping</param>
        /// <returns>The cell for the scroller to use</returns>
        public virtual EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {
            // first, we get a cell from the scroller by passing a prefab.
            // if the scroller finds one it can recycle it will do so, otherwise
            // it will create a new cell.
            CellView cellView = scroller.GetCellView(cellViewPrefab) as CellView;

            // set the name of the game object to the cell's data index.
            // this is optional, but it helps up debug the objects in 
            // the scene hierarchy.
            cellView.name = "Cell " + dataIndex.ToString();

            // in this example, we just pass the data to our cell's view which will update its UI
            cellView.SetData(_data[dataIndex]);

            // return the cell to the scroller
            return cellView;
        }

        #endregion
    }
}
