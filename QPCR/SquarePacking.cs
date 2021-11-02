using System;
using System.Collections.Generic;
using System.Linq;

namespace QPCR
{
    enum SortDirection
    {
        Ascending,
        Descending
    }

    public class SquarePacking
    {
        List<PackingElements> _elements = new List<PackingElements>();
        List<PackedElements> _packedElements = new List<PackedElements>();

        List<PackingPosition> _positionList = new List<PackingPosition>();
        List<PackingPosition> _unfilledPositionList = new List<PackingPosition>();

        Plate _plate = new Plate(12, 8);
        int plateCounter = 1;

        public Plate Plate {  get { return _plate; } set { _plate = value; } }

        public SquarePacking(List<PackingElements> packingElements)
        {
            _elements = packingElements;

            // Sort elements descending by his characteristics
            // Sort in descending oreder bigger first
            _elements = ElementsSort(_elements);
        }

        public SquarePacking(string[][] elements)
        {
            List<PackingElements>  packingElements = new List<PackingElements>();
            var i = 0;
            foreach(var str in elements)
            {
                // Split reagents
                foreach(var reagent in str[1].Split(',')){
                    var pe = new PackingElements();
                    pe.Index = i;
                    pe.Width = int.Parse(str[2]); // Replications
                    pe.Height = str[0].Trim().Split(',').Length; // Samples
                    pe.Samples = str[0].Trim().Split(',');
                    pe.Reagent = reagent;
                    packingElements.Add(pe);
                    i++;
                }
            }

            _elements = packingElements;

            // Sort elements descending by his characteristics
            // Sort in descending oreder bigger first
            _elements = ElementsSort(_elements);
        }

        private List<PackingElements> ElementsSort(List<PackingElements> elements, SortDirection direction = SortDirection.Descending)
        {
            elements.Sort((a, b) =>
                a.FitFactor.CompareTo(b.FitFactor) == 0 ? // Fitting factor

                    a.Area.CompareTo(b.Area) == 0 ? // Compare by area

                        a.Width.CompareTo(b.Width) == 0 ? // Compare by width

                            a.Height.CompareTo(b.Height) // Compare by height

                        : a.Width.CompareTo(b.Width)

                    : a.Area.CompareTo(b.Area)

                : a.FitFactor.CompareTo(b.FitFactor)
            );

            int s = elements.Count - 1;
            elements = elements.Select(x => { x.sortOrder = s; --s; return x; }).ToList();

            if (direction == SortDirection.Descending)
            {
                elements = elements.OrderBy(o => o.sortOrder).ToList();
            }

            return elements;
        }

        private void PositionsSort(List<PackingPosition> positions)
        {
            // Sort positions first by Y, and then by X axes 
            positions.Sort((a, b) => a.Y.CompareTo(b.Y) == 0 ? a.X.CompareTo(b.X) : a.Y.CompareTo(b.Y));
        }

        //ToDo: Remove cyclomatic complexity
        public List<PackedElements> PackElements()
        {
            _unfilledPositionList = new List<PackingPosition>();
            int positionX = 0;
            int positionY = 0;

            // Postion is the whole plate at postion 0,0
            _positionList.Add(new PackingPosition()
            {
                Height = _plate.Height,
                Width= _plate.Width,
                X = positionX,
                Y = positionY
            });

            // Check fitting postion
            int elementCounter = _elements.Count;
            int packedElementsCounter = 0;
            int positionCounter = 1;
            while (positionCounter > 0 && elementCounter > 0)
            {
                // Sort positions first by Y, and then by X axes 
                PositionsSort(_positionList);

                // Position data is sorted and select by top weight
                int positionWidth = _positionList[0].Width;
                int positionHeight = _positionList[0].Height;

                int elementHeight;
                int elementWidth;

                int fittingFactor;
                // Fitting factor for start position
                #region Start Postion 
                for (var i = 0; i < elementCounter; i++)
                {
                    // Elemens are sorted already
                    elementHeight = _elements[i].Height;
                    elementWidth = _elements[i].Width;

                    // fitting initial
                    fittingFactor = 0;

                    // 1. Check Exact match
                    // 
                    if (positionWidth < elementWidth || positionHeight < elementHeight)
                    {
                        fittingFactor = 0;
                    }
                    else
                    {
                        // increase fitting factor
                        fittingFactor++;

                        // is exact width 
                        if (positionWidth == elementWidth)
                        {
                            // increase even more
                            fittingFactor++;
                        }

                        // is exact height
                        if (positionHeight == elementHeight)
                        {
                            fittingFactor++;
                        }
                    }

                    // Store fitting factor
                    _elements[i].FitFactor = fittingFactor;
                }
                #endregion

                // Now we have fitting factor - sort again
                _elements = ElementsSort(_elements);

                #region Store top fitting to packed list and remove
                // get the first with top weight
                fittingFactor = _elements.First().FitFactor;

                var positionOk = false;

                if (fittingFactor > 0)
                {
                    // ToDo: put in class 
                    positionX = _positionList[0].X;
                    positionY = _positionList[0].Y;

                    int elementIndex = _elements.First().Index;
                    elementHeight = _elements.First().Height;
                    elementWidth = _elements.First().Width;

                    // pack element since fitting factor > 0
                    _packedElements.Add(new PackedElements()
                    {
                        Height = elementHeight,
                        Width = elementWidth,
                        Index = elementIndex,
                        X = positionX,
                        Y = positionY,
                        PlateNumber = plateCounter,
                        Samples = _elements.First().Samples,
                        Reagent = _elements.First().Reagent
                    });

                    packedElementsCounter++;

                    // Remove packed element
                    _elements.Remove(_elements.First());
                    // Decrease counter
                    elementCounter--;

                    #region find positions
                    // Continue if needed
                    if (elementCounter > 0)
                    {
                        // Sort by fitting 
                        _elements = ElementsSort(_elements);

                        // Find positions --  add them to list
                        PackingPosition packingPosition = null;
                        // Top left position
                        if (positionHeight > elementHeight)
                        {
                            packingPosition = new PackingPosition()
                            {
                                Height = positionHeight - elementHeight,
                                Width = _plate.Width - positionX,
                                X = positionX,
                                Y = positionY + elementHeight, // y + packed element height
                            };
                            
                            Console.WriteLine($"Upper: Height: {packingPosition.Height}, Width: {packingPosition.Width}, X: {packingPosition.X}, Y: {packingPosition.Y}");

                            // Search for valid element

                            // Get area of the first element in table with smaler area
                            foreach (var el in _elements.OrderBy(x=>x.Area))
                            {
                                // check element
                                if (packingPosition.Width >= el.Width && packingPosition.Height >= el.Height)
                                {
                                    // store postion
                                    _positionList.Add(packingPosition);

                                    // Increase positions counter
                                    positionCounter++;

                                    // Set new position 'is adequate' state flag
                                    positionOk = true;

                                    break;
                                }
                            }
                        }

                        // Lower right position
                        if (positionWidth > elementWidth)
                        {
                            packingPosition = new PackingPosition()
                            {
                                Height = elementHeight,
                                Width = _plate.Width - (positionX + elementWidth),
                                X = positionX + elementWidth, // x + packed element width
                                Y = positionY,
                            };

                            if (!positionOk)
                            {
                                packingPosition.Height = positionHeight;
                            }
                            
                            Console.WriteLine($"Lower: Height: {packingPosition.Height}, Width: {packingPosition.Width}, X: {packingPosition.X}, Y: {packingPosition.Y}");

                            // Get area of the first element in table with smaler area
                            foreach (var el in _elements.OrderBy(x => x.Area))
                            {
                                // check element
                                if (packingPosition.Width >= el.Width && packingPosition.Height >= el.Height)
                                {
                                    // store postion
                                    _positionList.Add(packingPosition);

                                    // Increase positions counter
                                    positionCounter++;

                                    // Set new position 'is adequate' state flag
                                    positionOk = true;

                                    break;
                                }
                            }

                        }

                        if (packingPosition != null && !positionOk )
                        {
                            _unfilledPositionList.Add(packingPosition);
                        }
                    }
                    #endregion
                }
                #endregion

                if (elementCounter > 0)
                {
                    // Position not filled
                    if (fittingFactor == 0)
                    {
                        _unfilledPositionList.Add(_positionList[0]);
                    }

                    // since sorted ascending
                    _positionList.RemoveAt(0);

                    // decrease position counter
                    positionCounter--;
                }

            }

            // Sill more elements
            while (_elements.Count > 0 && _unfilledPositionList.Count > 0) {
                // Rearange the to fill positions
                var fitElement = ElementsSort(_elements).First();
                // Get biggest postion
                var fillPostition = _unfilledPositionList.OrderByDescending(x => x.Area).First();

                // fille and
                // rearange

                // the element ara is bigger than position
                if (fitElement.Area > fillPostition.Area)
                {
                    // Pack the new element
                    _packedElements.Add(new PackedElements() { 
                        Height = fillPostition.Height,
                        Width = fillPostition.Width,
                        Index = _elements.Count + 1,
                        X = fillPostition.X,
                        Y = fillPostition.Y,
                        PlateNumber = plateCounter,
                        Samples = fitElement.Samples,
                        Reagent = fitElement.Reagent
                    });
                    _unfilledPositionList.Remove(fillPostition);
                    var newArea = fitElement.Area - fillPostition.Area;

                    // the new element should fit the available or make half of plate
                    if (_unfilledPositionList.Count > 0)
                    {
                        fillPostition = _unfilledPositionList.OrderByDescending(x => x.Area).First();
                        var tmpWidht = fillPostition.Width;
                        var tmpHeight = newArea / tmpWidht;
                        _elements.Remove(fitElement);
                        _elements.Add(new PackingElements() {
                            Width = tmpWidht,
                            Height = tmpHeight,
                            Index = fitElement.Index + 1,
                            Samples = fitElement.Samples,
                            Reagent = fitElement.Reagent
                        });
                    }
                }

                // Reshape
                if (fitElement.Area < fillPostition.Area)
                {

                    _packedElements.Add(new PackedElements()
                    {
                        Height = fitElement.Area / fillPostition.Width,
                        Width = fillPostition.Width,
                        Index = _elements.Count + 1,
                        X = fillPostition.X,
                        Y = fillPostition.Y,
                        PlateNumber = plateCounter,
                        Samples = fitElement.Samples,
                        Reagent = fitElement.Reagent
                    });

                    fillPostition.Y = fillPostition.Y + (fitElement.Area / fillPostition.Width);
                    fillPostition.Height = fillPostition.Height - (fitElement.Area / fillPostition.Width);

                    _elements.Remove(fitElement);

                }
            }
            if (_elements.Count > 0)
            {
                plateCounter++;
                return PackElements();
            }

            return _packedElements;
        }

        public string[][] ArrangePlateElements()
        {
            string[][] plateGrid = new string[_plate.Height][];
            for (int h = 0; h < _plate.Height; h++)
            {
                string[] pl = new string[_plate.Width];
                for (int i = 0; i < _plate.Width; i++)
                {
                    var el = _packedElements.Where(x => x.X <= i && 
                    i < x.X + x.Width && 
                    x.Y <= h &&
                    h < x.Y + x.Height).FirstOrDefault();
                    string outputStr;
                    if (el == null)
                    {
                        outputStr = "[null, null]";
                    }
                    else
                    {
                        outputStr = $"[{el.Samples[el.Height - ((h-el.Y) + 1)]}, {el.Reagent}]";
                    }


                    pl[i] = outputStr;
                }
                plateGrid[h] = pl;
            }

            return plateGrid;
        }
    }
}
